using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Webhooks;

/// <summary>
/// Strongly typed model for Brevo transactional email webhook payloads.
/// </summary>
public sealed class BrevoEmailWebhookPayload
{
    /// <summary>
    /// The type of event (e.g. delivered, opened, click).
    /// </summary>
    [JsonPropertyName("event")]
    public BrevoEmailWebhookEvent Event { get; set; }

    /// <summary>
    /// Recipient email address.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// The message ID of the transactional email.
    /// </summary>
    [JsonPropertyName("message-id")]
    public string? MessageId { get; set; }

    /// <summary>
    /// UTC timestamp of the event.
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>
    /// Timestamp as a Unix epoch value.
    /// </summary>
    [JsonPropertyName("ts")]
    public long? Timestamp { get; set; }

    /// <summary>
    /// Timestamp as an epoch value (milliseconds).
    /// </summary>
    [JsonPropertyName("ts_epoch")]
    public long? TimestampEpoch { get; set; }

    /// <summary>
    /// Subject of the email.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Tag associated with the email.
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// The sending IP address.
    /// </summary>
    [JsonPropertyName("sending_ip")]
    public string? SendingIp { get; set; }

    /// <summary>
    /// Template ID used for the email, if applicable.
    /// </summary>
    [JsonPropertyName("template_id")]
    public long? TemplateId { get; set; }

    /// <summary>
    /// The URL clicked by the recipient (only for click events).
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Reason for a bounce or block, if applicable.
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    /// <summary>
    /// Custom headers included in the original email.
    /// </summary>
    [JsonPropertyName("X-Mailin-custom")]
    public string? CustomHeader { get; set; }
}
