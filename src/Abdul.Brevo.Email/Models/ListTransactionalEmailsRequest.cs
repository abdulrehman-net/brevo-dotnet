using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Query parameters for listing transactional emails via <c>GET /v3/smtp/emails</c>.
/// </summary>
public sealed class ListTransactionalEmailsRequest
{
    /// <summary>
    /// Filter by sender email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Filter by template ID.
    /// </summary>
    public long? TemplateId { get; set; }

    /// <summary>
    /// Filter by message ID.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Start date for filtering (YYYY-MM-DD format).
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// End date for filtering (YYYY-MM-DD format).
    /// </summary>
    public string? EndDate { get; set; }

    /// <summary>
    /// Sort order: <c>asc</c> or <c>desc</c>. Defaults to <c>desc</c>.
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Number of results to return (max 1000). Defaults to 500.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Offset for pagination. Defaults to 0.
    /// </summary>
    public int? Offset { get; set; }

    /// <summary>
    /// Builds the query string for the API request.
    /// </summary>
    internal string ToQueryString()
    {
        var parameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(Email))
            parameters.Add($"email={Uri.EscapeDataString(Email)}");

        if (TemplateId.HasValue)
            parameters.Add($"templateId={TemplateId.Value}");

        if (!string.IsNullOrWhiteSpace(MessageId))
            parameters.Add($"messageId={Uri.EscapeDataString(MessageId)}");

        if (!string.IsNullOrWhiteSpace(StartDate))
            parameters.Add($"startDate={Uri.EscapeDataString(StartDate)}");

        if (!string.IsNullOrWhiteSpace(EndDate))
            parameters.Add($"endDate={Uri.EscapeDataString(EndDate)}");

        if (!string.IsNullOrWhiteSpace(Sort))
            parameters.Add($"sort={Uri.EscapeDataString(Sort)}");

        if (Limit.HasValue)
            parameters.Add($"limit={Limit.Value}");

        if (Offset.HasValue)
            parameters.Add($"offset={Offset.Value}");

        return parameters.Count > 0
            ? "?" + string.Join("&", parameters)
            : string.Empty;
    }
}
