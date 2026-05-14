using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Abdul.Brevo.Abstractions.Exceptions;
using Abdul.Brevo.Abstractions.RateLimiting;

namespace Abdul.Brevo.Abstractions.Http;

/// <summary>
/// Abstract base HTTP client for all Brevo SDK modules. Provides authenticated
/// request creation, JSON serialization, structured error parsing, and
/// rate-limit header extraction.
/// </summary>
public abstract class BrevoHttpClientBase
{
    /// <summary>
    /// Shared JSON serialization options.
    /// </summary>
    protected static JsonSerializerOptions JsonOptions => BrevoJsonDefaults.Options;

    private readonly HttpClient _httpClient;
    private readonly BrevoOptionsBase _options;

    /// <summary>
    /// Initializes a new instance of the HTTP client base.
    /// </summary>
    protected BrevoHttpClientBase(HttpClient httpClient, BrevoOptionsBase options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>Sends a GET request and deserializes the response.</summary>
    public Task<TResponse> GetAsync<TResponse>(
        string path,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<object, TResponse>(
            HttpMethod.Get,
            path,
            body: null,
            cancellationToken);
    }

    /// <summary>Sends a POST request with a JSON body and deserializes the response.</summary>
    public Task<TResponse> PostAsync<TRequest, TResponse>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Post,
            path,
            body,
            cancellationToken);
    }

    /// <summary>Sends a PUT request with a JSON body and deserializes the response.</summary>
    public Task<TResponse> PutAsync<TRequest, TResponse>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Put,
            path,
            body,
            cancellationToken);
    }

    /// <summary>Sends a PATCH request with a JSON body and deserializes the response.</summary>
    public Task<TResponse> PatchAsync<TRequest, TResponse>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Patch,
            path,
            body,
            cancellationToken);
    }

    /// <summary>Sends a POST request and expects no response body (204/2xx).</summary>
    public async Task PostNoContentAsync<TRequest>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Post, path);
        request.Content = JsonContent.Create(body, options: JsonOptions);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        await ThrowIfFailedAsync(response, responseBody);
    }

    /// <summary>Sends a PATCH request and expects no response body (204/2xx).</summary>
    public async Task PatchNoContentAsync<TRequest>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Patch, path);
        request.Content = JsonContent.Create(body, options: JsonOptions);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        await ThrowIfFailedAsync(response, responseBody);
    }

    /// <summary>Sends a DELETE request and expects no response body.</summary>
    public async Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Delete, path);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        await ThrowIfFailedAsync(response, responseBody);
    }

    /// <summary>Sends a POST request with multipart form data and deserializes the response.</summary>
    public async Task<TResponse> PostMultipartAsync<TResponse>(
        string path,
        MultipartFormDataContent content,
        CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Post, path);
        request.Content = content;

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        await ThrowIfFailedAsync(response, responseBody);

        var result = JsonSerializer.Deserialize<TResponse>(responseBody, JsonOptions);

        return result ?? throw CreateApiException(
            (int)response.StatusCode,
            response.ReasonPhrase,
            "Brevo returned an empty or invalid response body.",
            responseBody,
            null,
            null);
    }

    /// <summary>
    /// Core send method that creates a request, optionally attaches a JSON body,
    /// sends it, validates the response, and deserializes the result.
    /// </summary>
    protected virtual async Task<TResponse> SendAsync<TRequest, TResponse>(
        HttpMethod method,
        string path,
        TRequest? body,
        CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, path);

        if (body is not null)
            request.Content = JsonContent.Create(body, options: JsonOptions);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        await ThrowIfFailedAsync(response, responseBody);

        var result = JsonSerializer.Deserialize<TResponse>(responseBody, JsonOptions);

        return result ?? throw CreateApiException(
            statusCode: (int)response.StatusCode,
            reasonPhrase: response.ReasonPhrase,
            message: "Brevo returned an empty or invalid response body.",
            responseBody: responseBody,
            brevoCode: null,
            brevoMessage: null);
    }

    /// <summary>
    /// Inspects the HTTP response and throws the appropriate
    /// <see cref="BrevoApiException"/> subclass on failure. Override this in
    /// domain SDKs to handle domain-specific error conditions.
    /// </summary>
    protected virtual Task ThrowIfFailedAsync(
        HttpResponseMessage response,
        string responseBody)
    {
        if (response.IsSuccessStatusCode)
            return Task.CompletedTask;

        BrevoErrorResponse? brevoError = null;

        try
        {
            brevoError = JsonSerializer.Deserialize<BrevoErrorResponse>(
                responseBody,
                JsonOptions);
        }
        catch
        {
            // Ignore invalid/non-JSON error bodies.
        }

        var message = brevoError?.Message
                      ?? $"Brevo API request failed with status code {(int)response.StatusCode}.";

        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            var rateLimitInfo = BrevoRateLimitInfo.FromResponse(response);

            throw new BrevoRateLimitException(
                message: message,
                responseBody: responseBody,
                brevoCode: brevoError?.Code,
                rateLimitInfo: rateLimitInfo);
        }

        if (response.StatusCode == HttpStatusCode.PaymentRequired)
        {
            throw new BrevoPaymentRequiredException(
                message: message,
                brevoCode: brevoError?.Code,
                responseBody: responseBody);
        }

        throw CreateApiException(
            statusCode: (int)response.StatusCode,
            reasonPhrase: response.ReasonPhrase,
            message: message,
            responseBody: responseBody,
            brevoCode: brevoError?.Code,
            brevoMessage: brevoError?.Message);
    }

    /// <summary>
    /// Creates a domain-specific API exception. Override this in derived clients
    /// to return types like <c>BrevoEmailApiException</c>.
    /// </summary>
    protected virtual BrevoApiException CreateApiException(
        int statusCode,
        string? reasonPhrase,
        string message,
        string? responseBody,
        string? brevoCode,
        string? brevoMessage)
    {
        return new BrevoApiException(
            statusCode,
            reasonPhrase,
            message,
            responseBody,
            brevoCode,
            brevoMessage);
    }

    /// <summary>
    /// Creates an authenticated <see cref="HttpRequestMessage"/> with the
    /// <c>api-key</c> header and JSON accept header.
    /// </summary>
    protected HttpRequestMessage CreateRequest(HttpMethod method, string path)
    {
        var request = new HttpRequestMessage(method, path);

        request.Headers.Add("api-key", _options.ApiKey);
        request.Headers.Accept.ParseAdd("application/json");

        return request;
    }
}
