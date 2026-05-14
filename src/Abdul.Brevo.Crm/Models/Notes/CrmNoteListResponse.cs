using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Notes;

/// <summary>
/// Response from listing notes.
/// </summary>
public sealed class CrmNoteListResponse
{
    /// <summary>
    /// Total number of notes matching the filter.
    /// </summary>
    [JsonPropertyName("count")]
    public long? Count { get; set; }

    /// <summary>
    /// List of notes.
    /// </summary>
    [JsonPropertyName("notes")]
    public List<CrmNote>? Notes { get; set; }
}
