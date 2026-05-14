using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Files;

/// <summary>
/// Response containing a temporary download URL for a CRM file.
/// </summary>
public sealed class FileDownloadResponse
{
    /// <summary>
    /// Temporary URL to download the file (valid for 5 minutes).
    /// </summary>
    [JsonPropertyName("fileUrl")]
    public string? FileUrl { get; set; }
}
