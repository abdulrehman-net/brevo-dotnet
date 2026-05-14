using Abdul.Brevo.Crm.Models.Deals;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing deals in the Brevo Sales CRM.
/// </summary>
public interface IBrevoCrmDealsClient
{
    /// <summary>
    /// Gets a paginated list of deals based on filters.
    /// </summary>
    Task<DealListResponse> ListAsync(
        ListDealsRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new deal.
    /// </summary>
    Task<Deal> CreateAsync(
        CreateDealRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets full details of a specific deal.
    /// </summary>
    Task<Deal> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing deal.
    /// </summary>
    Task UpdateAsync(
        string id,
        UpdateDealRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a deal.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Links or unlinks contacts and companies to a deal.
    /// </summary>
    Task LinkUnlinkAsync(
        string id,
        DealLinkUnlinkRequest request,
        CancellationToken cancellationToken = default);
}
