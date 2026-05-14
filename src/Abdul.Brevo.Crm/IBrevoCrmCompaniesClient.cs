using Abdul.Brevo.Crm.Models.Common;
using Abdul.Brevo.Crm.Models.Companies;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing companies in the Brevo Sales CRM.
/// </summary>
public interface IBrevoCrmCompaniesClient
{
    /// <summary>
    /// Gets a paginated list of companies based on filters.
    /// </summary>
    Task<CompanyListResponse> ListAsync(
        ListCompaniesRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new company.
    /// </summary>
    Task<Company> CreateAsync(
        CreateCompanyRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets full details of a specific company.
    /// </summary>
    Task<Company> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    Task UpdateAsync(
        string id,
        UpdateCompanyRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a company.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Links or unlinks contacts and deals to a company.
    /// </summary>
    Task LinkUnlinkAsync(
        string id,
        LinkUnlinkRequest request,
        CancellationToken cancellationToken = default);
}
