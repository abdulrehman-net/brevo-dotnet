using System.Text.Json.Serialization;
using Abdul.Brevo.Core.Models;

namespace Abdul.Brevo.Core;

/// <summary>
/// Client for interacting with the Brevo Webhooks API.
/// </summary>
public interface IBrevoWebhooksClient
{
    /// <summary>
    /// Retrieves a list of all configured webhooks.
    /// </summary>
    Task<BrevoWebhooksResponse> ListAsync(
        GetWebhooksRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new webhook.
    /// </summary>
    Task<BrevoWebhook> CreateAsync(
        CreateWebhookRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a webhook by its ID.
    /// </summary>
    Task<BrevoWebhook> GetAsync(
        long webhookId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a webhook by its ID.
    /// </summary>
    Task UpdateAsync(
        long webhookId,
        UpdateWebhookRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a webhook by its ID.
    /// </summary>
    Task DeleteAsync(
        long webhookId,
        CancellationToken cancellationToken = default);
}

internal sealed class BrevoWebhooksClient : IBrevoWebhooksClient
{
    private readonly BrevoCoreHttpClient _httpClient;

    public BrevoWebhooksClient(BrevoCoreHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<BrevoWebhooksResponse> ListAsync(
        GetWebhooksRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var path = "v3/webhooks";
        if (request != null)
        {
            path = request.AppendTo(path);
        }

        return _httpClient.GetAsync<BrevoWebhooksResponse>(path, cancellationToken);
    }

    public async Task<BrevoWebhook> CreateAsync(
        CreateWebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.PostAsync<CreateWebhookRequest, CreateWebhookResponse>("v3/webhooks", request, cancellationToken);
        return await GetAsync(result.Id, cancellationToken);
    }

    public Task<BrevoWebhook> GetAsync(
        long webhookId,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.GetAsync<BrevoWebhook>($"v3/webhooks/{webhookId}", cancellationToken);
    }

    public Task UpdateAsync(
        long webhookId,
        UpdateWebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PutAsync<UpdateWebhookRequest, object>($"v3/webhooks/{webhookId}", request, cancellationToken);
    }

    public Task DeleteAsync(
        long webhookId,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.DeleteAsync($"v3/webhooks/{webhookId}", cancellationToken);
    }
}

public sealed class BrevoWebhooksResponse
{
    [JsonPropertyName("webhooks")]
    public IReadOnlyList<BrevoWebhook> Webhooks { get; init; } = [];
}

internal sealed class CreateWebhookResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
}
