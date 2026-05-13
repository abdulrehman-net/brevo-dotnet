using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace Nexin.Brevo.Conversations;

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

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new BrevoConversationsApiException(response.StatusCode, responseBody);
        }
    }

    public async Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Delete, path);

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new BrevoConversationsApiException(response.StatusCode, responseBody);
        }
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

        if (!response.IsSuccessStatusCode)
            throw new BrevoConversationsApiException(response.StatusCode, responseBody);

        var result = JsonSerializer.Deserialize<TResponse>(responseBody, JsonOptions);

        return result ?? throw new BrevoConversationsApiException(response.StatusCode, responseBody);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string path)
    {
        var request = new HttpRequestMessage(method, path);

        request.Headers.Add("api-key", _options.ApiKey);
        request.Headers.Accept.ParseAdd("application/json");

        return request;
    }
}
