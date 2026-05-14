using Abdul.Brevo.Abstractions.RateLimiting;

namespace Abdul.Brevo.Abstractions.Exceptions;

/// <summary>
/// Thrown when Brevo returns HTTP 429 Too Many Requests. Carries the parsed
/// rate-limit headers so consumers can implement retry/backoff strategies.
/// </summary>
public class BrevoRateLimitException : BrevoApiException
{
    /// <summary>
    /// Gets the parsed rate-limit information from the response headers.
    /// </summary>
    public BrevoRateLimitInfo? RateLimitInfo { get; }

    /// <summary>
    /// Gets the suggested wait duration before retrying, derived from the
    /// <c>x-sib-ratelimit-reset</c> header.
    /// </summary>
    public TimeSpan? RetryAfter { get; }

    public BrevoRateLimitException(
        string message,
        string? responseBody,
        string? brevoCode = null,
        BrevoRateLimitInfo? rateLimitInfo = null)
        : base(
            statusCode: 429,
            reasonPhrase: "TooManyRequests",
            message: message,
            responseBody: responseBody,
            brevoCode: brevoCode)
    {
        RateLimitInfo = rateLimitInfo;
        RetryAfter = rateLimitInfo?.ResetSeconds is > 0
            ? TimeSpan.FromSeconds(rateLimitInfo.ResetSeconds.Value)
            : null;
    }
}
