using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Files;

/// <summary>
/// Response from listing CRM files.
/// </summary>
public sealed class CrmFileListResponse
{
    /// <summary>
    /// List of files.
    /// </summary>
    [JsonPropertyName("files")]
    public List<CrmFile>? Files { get; set; }
}
