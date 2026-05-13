using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Paginated response from listing transactional emails.
/// </summary>
public sealed class TransactionalEmailListResponse
{
    /// <summary>
    /// Total count of transactional emails matching the filters.
    /// </summary>
    [JsonPropertyName("count")]
    public long? Count { get; set; }

    /// <summary>
    /// List of transactional email summaries.
    /// </summary>
    [JsonPropertyName("transactionalEmails")]
    public List<TransactionalEmailSummary>? TransactionalEmails { get; set; }
}
