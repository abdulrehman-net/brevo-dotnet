using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Notes;

/// <summary>
/// Represents a CRM note.
/// </summary>
public sealed class CrmNote
{
    /// <summary>
    /// Unique identifier for the note.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Content of the note (supports basic HTML).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// IDs of contacts linked to this note.
    /// </summary>
    [JsonPropertyName("contactIds")]
    public List<long>? ContactIds { get; set; }

    /// <summary>
    /// IDs of deals linked to this note.
    /// </summary>
    [JsonPropertyName("dealIds")]
    public List<string>? DealIds { get; set; }

    /// <summary>
    /// IDs of companies linked to this note.
    /// </summary>
    [JsonPropertyName("companyIds")]
    public List<string>? CompanyIds { get; set; }

    /// <summary>
    /// Date and time when the note was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// Date and time when the note was last updated.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
