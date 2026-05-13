using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Response from sending a transactional email via <c>POST /v3/smtp/email</c>.
/// </summary>
public sealed class SendTransactionalEmailResponse
{
    /// <summary>
    /// The message ID assigned by Brevo (e.g. <c>&lt;201798300811.5787683@relay.domain.com&gt;</c>).
    /// </summary>
    [JsonPropertyName("messageId")]
    public string? MessageId { get; set; }

    /// <summary>
    /// Message IDs for batch/versioned sends — one per message version.
    /// </summary>
    [JsonPropertyName("messageIds")]
    public List<string>? MessageIds { get; set; }
}
