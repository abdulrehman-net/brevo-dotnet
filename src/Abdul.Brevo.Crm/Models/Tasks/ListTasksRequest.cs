namespace Abdul.Brevo.Crm.Models.Tasks;

/// <summary>
/// Query parameters for listing tasks.
/// </summary>
public sealed class ListTasksRequest
{
    /// <summary>
    /// Filter by task type ID.
    /// </summary>
    public string? FilterTaskTypeId { get; set; }

    /// <summary>
    /// Filter by task status (true/false).
    /// </summary>
    public bool? FilterDone { get; set; }

    /// <summary>
    /// Filter by contact ID.
    /// </summary>
    public long? FilterContactId { get; set; }

    /// <summary>
    /// Filter by deal ID.
    /// </summary>
    public string? FilterDealId { get; set; }

    /// <summary>
    /// Filter by company ID.
    /// </summary>
    public string? FilterCompanyId { get; set; }

    /// <summary>
    /// Filter by assigned user ID.
    /// </summary>
    public string? FilterAssignToId { get; set; }

    /// <summary>
    /// Filter tasks due after a specific date (timestamp in milliseconds).
    /// </summary>
    public long? DateFrom { get; set; }

    /// <summary>
    /// Filter tasks due before a specific date (timestamp in milliseconds).
    /// </summary>
    public long? DateTo { get; set; }

    /// <summary>
    /// Index of the first document of the page.
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Number of documents per page (max 100). Defaults to 50.
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Sort order: asc or desc. Defaults to desc.
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Builds the query string for the API request.
    /// </summary>
    internal string ToQueryString()
    {
        var parameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(FilterTaskTypeId))
            parameters.Add($"filter[taskTypeId]={Uri.EscapeDataString(FilterTaskTypeId)}");

        if (FilterDone.HasValue)
            parameters.Add($"filter[done]={FilterDone.Value.ToString().ToLower()}");

        if (FilterContactId.HasValue)
            parameters.Add($"filter[contactId]={FilterContactId.Value}");

        if (!string.IsNullOrWhiteSpace(FilterDealId))
            parameters.Add($"filter[dealId]={Uri.EscapeDataString(FilterDealId)}");

        if (!string.IsNullOrWhiteSpace(FilterCompanyId))
            parameters.Add($"filter[companyId]={Uri.EscapeDataString(FilterCompanyId)}");

        if (!string.IsNullOrWhiteSpace(FilterAssignToId))
            parameters.Add($"filter[assignToId]={Uri.EscapeDataString(FilterAssignToId)}");

        if (DateFrom.HasValue)
            parameters.Add($"dateFrom={DateFrom.Value}");

        if (DateTo.HasValue)
            parameters.Add($"dateTo={DateTo.Value}");

        if (Offset.HasValue)
            parameters.Add($"offset={Offset.Value}");

        if (Limit.HasValue)
            parameters.Add($"limit={Limit.Value}");

        if (!string.IsNullOrWhiteSpace(Sort))
            parameters.Add($"sort={Uri.EscapeDataString(Sort)}");

        return parameters.Count > 0
            ? "?" + string.Join("&", parameters)
            : string.Empty;
    }
}
