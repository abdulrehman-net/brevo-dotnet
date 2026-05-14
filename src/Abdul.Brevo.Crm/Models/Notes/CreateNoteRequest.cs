using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Notes;

/// <summary>
/// Request for creating a new CRM note.
/// </summary>
public sealed class CreateNoteRequest
{
    /// <summary>
    /// Content of the note (supports basic HTML).
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// IDs of contacts to link to the note.
    /// </summary>
    [JsonPropertyName("contactIds")]
    public List<long>? ContactIds { get; set; }

    /// <summary>
    /// IDs of deals to link to the note.
    /// </summary>
    [JsonPropertyName("dealIds")]
    public List<string>? DealIds { get; set; }

    /// <summary>
    /// IDs of companies to link to the note.
    /// </summary>
    [JsonPropertyName("companyIds")]
    public List<string>? CompanyIds { get; set; }
}
