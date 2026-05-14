namespace Abdul.Brevo.Crm.Models.Companies;

/// <summary>
/// Query parameters for listing companies.
/// </summary>
public sealed class ListCompaniesRequest
{
    /// <summary>
    /// Filter by company name.
    /// </summary>
    public string? FiltersAttributesName { get; set; }

    /// <summary>
    /// Filter by company owner (account email).
    /// </summary>
    public string? FiltersAttributesOwner { get; set; }

    /// <summary>
    /// Filter by linked contact ID.
    /// </summary>
    public long? LinkedContactsIds { get; set; }

    /// <summary>
    /// Filter by linked deal ID.
    /// </summary>
    public string? LinkedDealsIds { get; set; }

    /// <summary>
    /// Filter companies created after a specific UTC date-time (format: YYYY-MM-DDTHH:mm:ss.SSSZ).
    /// </summary>
    public string? CreatedSince { get; set; }

    /// <summary>
    /// Filter companies modified after a specific UTC date-time (format: YYYY-MM-DDTHH:mm:ss.SSSZ).
    /// </summary>
    public string? ModifiedSince { get; set; }

    /// <summary>
    /// Index of the first document of the page.
    /// </summary>
    public long? Page { get; set; }

    /// <summary>
    /// Number of documents per page (max 1000). Defaults to 50.
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// The field name used to sort the results.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Builds the query string for the API request.
    /// </summary>
    internal string ToQueryString()
    {
        var parameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(FiltersAttributesName))
            parameters.Add($"filters[attributes.name]={Uri.EscapeDataString(FiltersAttributesName)}");

        if (!string.IsNullOrWhiteSpace(FiltersAttributesOwner))
            parameters.Add($"filters[attributes.owner]={Uri.EscapeDataString(FiltersAttributesOwner)}");

        if (LinkedContactsIds.HasValue)
            parameters.Add($"linkedContactsIds={LinkedContactsIds.Value}");

        if (!string.IsNullOrWhiteSpace(LinkedDealsIds))
            parameters.Add($"linkedDealsIds={Uri.EscapeDataString(LinkedDealsIds)}");

        if (!string.IsNullOrWhiteSpace(CreatedSince))
            parameters.Add($"createdSince={Uri.EscapeDataString(CreatedSince)}");

        if (!string.IsNullOrWhiteSpace(ModifiedSince))
            parameters.Add($"modifiedSince={Uri.EscapeDataString(ModifiedSince)}");

        if (Page.HasValue)
            parameters.Add($"page={Page.Value}");

        if (Limit.HasValue)
            parameters.Add($"limit={Limit.Value}");

        if (!string.IsNullOrWhiteSpace(SortBy))
            parameters.Add($"sortBy={Uri.EscapeDataString(SortBy)}");

        return parameters.Count > 0
            ? "?" + string.Join("&", parameters)
            : string.Empty;
    }
}
