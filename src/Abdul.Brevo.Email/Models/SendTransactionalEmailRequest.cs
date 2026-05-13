using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Request body for sending a transactional email via <c>POST /v3/smtp/email</c>.
/// </summary>
public sealed class SendTransactionalEmailRequest
{
    /// <summary>
    /// Sender information. Required when <see cref="TemplateId"/> is not provided.
    /// </summary>
    [JsonPropertyName("sender")]
    public EmailSender? Sender { get; set; }

    /// <summary>
    /// List of primary recipients. Required when <see cref="MessageVersions"/> is not provided.
    /// </summary>
    [JsonPropertyName("to")]
    public List<EmailRecipient>? To { get; set; }

    /// <summary>
    /// BCC recipients.
    /// </summary>
    [JsonPropertyName("bcc")]
    public List<EmailRecipient>? Bcc { get; set; }

    /// <summary>
    /// CC recipients.
    /// </summary>
    [JsonPropertyName("cc")]
    public List<EmailRecipient>? Cc { get; set; }

    /// <summary>
    /// HTML body content. Required when <see cref="TemplateId"/> is not provided.
    /// </summary>
    [JsonPropertyName("htmlContent")]
    public string? HtmlContent { get; set; }

    /// <summary>
    /// Plain text body content. Ignored when <see cref="TemplateId"/> is provided.
    /// </summary>
    [JsonPropertyName("textContent")]
    public string? TextContent { get; set; }

    /// <summary>
    /// Email subject line. Required when <see cref="TemplateId"/> is not provided.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Reply-to address.
    /// </summary>
    [JsonPropertyName("replyTo")]
    public EmailReplyTo? ReplyTo { get; set; }

    /// <summary>
    /// Attachments to include with the email.
    /// </summary>
    [JsonPropertyName("attachment")]
    public List<EmailAttachment>? Attachment { get; set; }

    /// <summary>
    /// Custom email headers (non-standard headers only, e.g. <c>X-Mailin-custom</c>).
    /// </summary>
    [JsonPropertyName("headers")]
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// ID of a pre-built Brevo template to use.
    /// </summary>
    [JsonPropertyName("templateId")]
    public long? TemplateId { get; set; }

    /// <summary>
    /// Key-value pairs for template variable substitution (New Template Language format).
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object>? Params { get; set; }

    /// <summary>
    /// Message version objects for batch sending personalized variants.
    /// Max 2000 total recipients, 99 per version.
    /// </summary>
    [JsonPropertyName("messageVersions")]
    public List<MessageVersion>? MessageVersions { get; set; }

    /// <summary>
    /// Tag(s) for categorizing the email.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// UTC date-time to schedule the email (format: YYYY-MM-DDTHH:mm:ss.SSSZ).
    /// </summary>
    [JsonPropertyName("scheduledAt")]
    public DateTimeOffset? ScheduledAt { get; set; }

    /// <summary>
    /// Batch ID to group scheduled emails together.
    /// </summary>
    [JsonPropertyName("batchId")]
    public string? BatchId { get; set; }
}
