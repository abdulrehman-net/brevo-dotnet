using Abdul.Brevo.Crm.Models.Attributes;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing custom attributes in the Brevo Sales CRM.
/// </summary>
public interface IBrevoCrmAttributesClient
{
    /// <summary>
    /// Creates a new custom attribute for companies or deals.
    /// </summary>
    Task CreateAsync(
        CreateCrmAttributeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all defined attributes for companies.
    /// </summary>
    Task<List<CrmAttribute>> ListCompanyAttributesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all defined attributes for deals.
    /// </summary>
    Task<List<CrmAttribute>> ListDealAttributesAsync(
        CancellationToken cancellationToken = default);
}
