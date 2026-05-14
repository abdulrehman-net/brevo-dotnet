using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Notes;

/// <summary>
/// Request for updating an existing CRM note.
/// </summary>
public sealed class UpdateNoteRequest
{
    /// <summary>
    /// Updated content of the note (supports basic HTML).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Updated IDs of contacts linked to the note.
    /// </summary>
    [JsonPropertyName("contactIds")]
    public List<long>? ContactIds { get; set; }

    /// <summary>
    /// Updated IDs of deals linked to the note.
    /// </summary>
    [JsonPropertyName("dealIds")]
    public List<string>? DealIds { get; set; }

    /// <summary>
    /// Updated IDs of companies linked to the note.
    /// </summary>
    [JsonPropertyName("companyIds")]
    public List<string>? CompanyIds { get; set; }
}
