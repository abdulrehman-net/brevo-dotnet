using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Webhooks;

/// <summary>
/// Webhook event types for transactional email events.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BrevoEmailWebhookEvent
{
    /// <summary>Email was delivered to the recipient.</summary>
    Delivered,

    /// <summary>Email soft-bounced.</summary>
    SoftBounce,

    /// <summary>Email hard-bounced.</summary>
    HardBounce,

    /// <summary>Email was marked as spam/complaint.</summary>
    Complaint,

    /// <summary>Email was opened by the recipient.</summary>
    Opened,

    /// <summary>A link in the email was clicked.</summary>
    Click,

    /// <summary>Recipient unsubscribed.</summary>
    Unsubscribed,

    /// <summary>Email was blocked before sending.</summary>
    Blocked,

    /// <summary>The email address was invalid.</summary>
    Invalid,

    /// <summary>Email was deferred and will be retried.</summary>
    Deferred,

    /// <summary>Sending error occurred.</summary>
    Error
}
