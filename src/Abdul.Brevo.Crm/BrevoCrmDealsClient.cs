using Abdul.Brevo.Crm.Models.Deals;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmDealsClient : IBrevoCrmDealsClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmDealsClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task<DealListResponse> ListAsync(
        ListDealsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = request?.ToQueryString() ?? string.Empty;

        return _client.GetAsync<DealListResponse>(
            $"/v3/crm/deals{queryString}",
            cancellationToken);
    }

    public Task<Deal> CreateAsync(
        CreateDealRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostAsync<CreateDealRequest, Deal>(
            "/v3/crm/deals",
            request,
            cancellationToken);
    }

    public Task<Deal> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.GetAsync<Deal>(
            $"/v3/crm/deals/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task UpdateAsync(
        string id,
        UpdateDealRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(request);

        return _client.PatchNoContentAsync(
            $"/v3/crm/deals/{Uri.EscapeDataString(id)}",
            request,
            cancellationToken);
    }

    public Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.DeleteAsync(
            $"/v3/crm/deals/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task LinkUnlinkAsync(
        string id,
        DealLinkUnlinkRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(request);

        return _client.PatchNoContentAsync(
            $"/v3/crm/deals/link-unlink/{Uri.EscapeDataString(id)}",
            request,
            cancellationToken);
    }
}
