using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Represents an email attachment. Provide either <see cref="Url"/> or <see cref="Content"/> (base64-encoded).
/// </summary>
public sealed class EmailAttachment
{
    /// <summary>
    /// Absolute URL to the attachment file. Cannot be a local file path.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Base64-encoded content of the attachment.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// Filename of the attachment. Required when <see cref="Content"/> is provided.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
