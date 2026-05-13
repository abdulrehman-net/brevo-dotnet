using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Conversations;

internal sealed class BrevoConversationsHttpClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly HttpClient _httpClient;
    private readonly BrevoConversationsOptions _options;

    public BrevoConversationsHttpClient(
        HttpClient httpClient,
        IOptions<BrevoConversationsOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

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

    public async Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Delete, path);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        await ThrowIfFailedAsync(response, responseBody);
    }

    private async Task<TResponse> SendAsync<TRequest, TResponse>(
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

        return result
               ?? throw new BrevoConversationsApiException(
                   statusCode: (int)response.StatusCode,
                   reasonPhrase: response.ReasonPhrase,
                   message: "Brevo returned an empty or invalid response body.",
                   responseBody: responseBody);
    }

    private static Task ThrowIfFailedAsync(
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

        if (response.StatusCode == HttpStatusCode.PaymentRequired)
        {
            throw new BrevoConversationsPaymentRequiredException(
                message: brevoError?.Message
                         ?? "Brevo returned 402 Payment Required. The Conversations REST API may require a paid plan, enabled feature access, or available credits.",
                brevoCode: brevoError?.Code,
                responseBody: responseBody);
        }

        throw new BrevoConversationsApiException(
            statusCode: (int)response.StatusCode,
            reasonPhrase: response.ReasonPhrase,
            message: brevoError?.Message
                     ?? $"Brevo Conversations API request failed with status code {(int)response.StatusCode}.",
            responseBody: responseBody);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string path)
    {
        var request = new HttpRequestMessage(method, path);

        request.Headers.Add("api-key", _options.ApiKey);
        request.Headers.Accept.ParseAdd("application/json");

        return request;
    }
}

