using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Represents a message version for batch sending customized email variants.
/// </summary>
public sealed class MessageVersion
{
    /// <summary>
    /// Recipients for this version (max 99).
    /// </summary>
    [JsonPropertyName("to")]
    public required List<EmailRecipient> To { get; set; }

    /// <summary>
    /// Key-value pairs for template variable substitution in this version.
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object>? Params { get; set; }

    /// <summary>
    /// BCC recipients for this version.
    /// </summary>
    [JsonPropertyName("bcc")]
    public List<EmailRecipient>? Bcc { get; set; }

    /// <summary>
    /// CC recipients for this version.
    /// </summary>
    [JsonPropertyName("cc")]
    public List<EmailRecipient>? Cc { get; set; }

    /// <summary>
    /// Reply-to address for this version.
    /// </summary>
    [JsonPropertyName("replyTo")]
    public EmailReplyTo? ReplyTo { get; set; }

    /// <summary>
    /// Subject line for this version (overrides the global subject).
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// HTML content for this version (overrides the global htmlContent).
    /// </summary>
    [JsonPropertyName("htmlContent")]
    public string? HtmlContent { get; set; }

    /// <summary>
    /// Plain text content for this version (overrides the global textContent).
    /// </summary>
    [JsonPropertyName("textContent")]
    public string? TextContent { get; set; }

    /// <summary>
    /// Template ID for this version (overrides the global templateId).
    /// </summary>
    [JsonPropertyName("templateId")]
    public long? TemplateId { get; set; }
}
