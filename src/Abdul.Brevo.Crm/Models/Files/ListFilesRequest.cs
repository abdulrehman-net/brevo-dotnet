namespace Abdul.Brevo.Crm.Models.Files;

/// <summary>
/// Query parameters for listing CRM files.
/// </summary>
public sealed class ListFilesRequest
{
    /// <summary>
    /// Filter by entity type (contacts, companies, or deals).
    /// </summary>
    public string? Entity { get; set; }

    /// <summary>
    /// Filter by specific entity IDs (comma-separated string).
    /// </summary>
    public string? EntityIds { get; set; }

    /// <summary>
    /// Filter by creation date range (timestamp in milliseconds).
    /// </summary>
    public long? DateFrom { get; set; }

    /// <summary>
    /// Filter by creation date range (timestamp in milliseconds).
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
    /// Sort order: asc or desc. Defaults to desc (by creation date).
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Builds the query string for the API request.
    /// </summary>
    internal string ToQueryString()
    {
        var parameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(Entity))
            parameters.Add($"entity={Uri.EscapeDataString(Entity)}");

        if (!string.IsNullOrWhiteSpace(EntityIds))
            parameters.Add($"entityIds={Uri.EscapeDataString(EntityIds)}");

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
