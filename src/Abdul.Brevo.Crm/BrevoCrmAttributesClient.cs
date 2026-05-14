using Abdul.Brevo.Crm.Models.Attributes;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmAttributesClient : IBrevoCrmAttributesClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmAttributesClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task CreateAsync(
        CreateCrmAttributeRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostNoContentAsync(
            "/v3/crm/attributes",
            request,
            cancellationToken);
    }

    public Task<List<CrmAttribute>> ListCompanyAttributesAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetAsync<List<CrmAttribute>>(
            "/v3/crm/attributes/companies",
            cancellationToken);
    }

    public Task<List<CrmAttribute>> ListDealAttributesAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetAsync<List<CrmAttribute>>(
            "/v3/crm/attributes/deals",
            cancellationToken);
    }
}
