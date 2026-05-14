namespace Abdul.Brevo.Conversations;

/// <summary>
/// Thrown when Brevo returns HTTP 402 Payment Required, indicating the
/// Conversations REST API operation requires a paid plan, enabled feature
/// access, or available credits.
/// </summary>
public sealed class BrevoConversationsPaymentRequiredException
    : BrevoConversationsApiException
{
    /// <summary>
    /// Gets the Brevo-specific error code (e.g. <c>not_enough_credits</c>).
    /// </summary>
    public new string? BrevoCode => base.BrevoCode;

    public BrevoConversationsPaymentRequiredException(
        string message,
        string? brevoCode,
        string? responseBody)
        : base(
            statusCode: 402,
            reasonPhrase: "PaymentRequired",
            message: message,
            responseBody: responseBody,
            brevoCode: brevoCode)
    {
    }
}
