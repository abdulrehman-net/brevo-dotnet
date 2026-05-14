namespace Abdul.Brevo.Abstractions.Exceptions;

/// <summary>
/// Thrown when Brevo returns HTTP 402 Payment Required, indicating the
/// API operation requires a paid plan, enabled feature access, or
/// available credits.
/// </summary>
public class BrevoPaymentRequiredException : BrevoApiException
{
    public BrevoPaymentRequiredException(
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
