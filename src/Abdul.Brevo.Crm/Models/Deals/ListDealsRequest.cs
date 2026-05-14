namespace Abdul.Brevo.Crm.Models.Deals;

/// <summary>
/// Query parameters for listing deals.
/// </summary>
public sealed class ListDealsRequest
{
    /// <summary>
    /// Filter by deal name.
    /// </summary>
    public string? FiltersAttributesDealName { get; set; }

    /// <summary>
    /// Filter by deal owner (account email).
    /// </summary>
    public string? FiltersAttributesDealOwner { get; set; }

    /// <summary>
    /// Filter by linked company ID.
    /// </summary>
    public string? FiltersLinkedCompaniesIds { get; set; }

    /// <summary>
    /// Filter by linked contact ID.
    /// </summary>
    public string? FiltersLinkedContactsIds { get; set; }

    /// <summary>
    /// Filter deals created after a specific UTC date-time (format: YYYY-MM-DDTHH:mm:ss.SSSZ).
    /// </summary>
    public string? CreatedSince { get; set; }

    /// <summary>
    /// Filter deals modified after a specific UTC date-time (format: YYYY-MM-DDTHH:mm:ss.SSSZ).
    /// </summary>
    public string? ModifiedSince { get; set; }

    /// <summary>
    /// Index of the first document of the page.
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Number of documents per page (max 1000). Defaults to 50.
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Sort order: asc or desc. Defaults to desc.
    /// </summary>
    public string? Sort { get; set; }

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

        if (!string.IsNullOrWhiteSpace(FiltersAttributesDealName))
            parameters.Add($"filters[attributes.deal_name]={Uri.EscapeDataString(FiltersAttributesDealName)}");

        if (!string.IsNullOrWhiteSpace(FiltersAttributesDealOwner))
            parameters.Add($"filters[attributes.deal_owner]={Uri.EscapeDataString(FiltersAttributesDealOwner)}");

        if (!string.IsNullOrWhiteSpace(FiltersLinkedCompaniesIds))
            parameters.Add($"filters[linkedCompaniesIds]={Uri.EscapeDataString(FiltersLinkedCompaniesIds)}");

        if (!string.IsNullOrWhiteSpace(FiltersLinkedContactsIds))
            parameters.Add($"filters[linkedContactsIds]={Uri.EscapeDataString(FiltersLinkedContactsIds)}");

        if (!string.IsNullOrWhiteSpace(CreatedSince))
            parameters.Add($"createdSince={Uri.EscapeDataString(CreatedSince)}");

        if (!string.IsNullOrWhiteSpace(ModifiedSince))
            parameters.Add($"modifiedSince={Uri.EscapeDataString(ModifiedSince)}");

        if (Offset.HasValue)
            parameters.Add($"offset={Offset.Value}");

        if (Limit.HasValue)
            parameters.Add($"limit={Limit.Value}");

        if (!string.IsNullOrWhiteSpace(Sort))
            parameters.Add($"sort={Uri.EscapeDataString(Sort)}");

        if (!string.IsNullOrWhiteSpace(SortBy))
            parameters.Add($"sortBy={Uri.EscapeDataString(SortBy)}");

        return parameters.Count > 0
            ? "?" + string.Join("&", parameters)
            : string.Empty;
    }
}
