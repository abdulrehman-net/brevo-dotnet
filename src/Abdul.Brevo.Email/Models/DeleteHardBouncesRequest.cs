using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Request body for deleting hard bounces via <c>POST /v3/smtp/deleteHardbounces</c>.
/// </summary>
public sealed class DeleteHardBouncesRequest
{
    /// <summary>
    /// Start date for deleting hard bounces (YYYY-MM-DD format).
    /// </summary>
    [JsonPropertyName("startDate")]
    public string? StartDate { get; set; }

    /// <summary>
    /// End date for deleting hard bounces (YYYY-MM-DD format).
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }

    /// <summary>
    /// Specific email address whose hard bounces should be deleted.
    /// </summary>
    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }
}
