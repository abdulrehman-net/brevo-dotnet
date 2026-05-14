using Abdul.Brevo.Crm.Models.Common;
using Abdul.Brevo.Crm.Models.Companies;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmCompaniesClient : IBrevoCrmCompaniesClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmCompaniesClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task<CompanyListResponse> ListAsync(
        ListCompaniesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = request?.ToQueryString() ?? string.Empty;

        return _client.GetAsync<CompanyListResponse>(
            $"/v3/companies{queryString}",
            cancellationToken);
    }

    public Task<Company> CreateAsync(
        CreateCompanyRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostAsync<CreateCompanyRequest, Company>(
            "/v3/companies",
            request,
            cancellationToken);
    }

    public Task<Company> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.GetAsync<Company>(
            $"/v3/companies/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task UpdateAsync(
        string id,
        UpdateCompanyRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(request);

        return _client.PatchNoContentAsync(
            $"/v3/companies/{Uri.EscapeDataString(id)}",
            request,
            cancellationToken);
    }

    public Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.DeleteAsync(
            $"/v3/companies/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task LinkUnlinkAsync(
        string id,
        LinkUnlinkRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(request);

        return _client.PatchNoContentAsync(
            $"/v3/companies/link-unlink/{Uri.EscapeDataString(id)}",
            request,
            cancellationToken);
    }
}
