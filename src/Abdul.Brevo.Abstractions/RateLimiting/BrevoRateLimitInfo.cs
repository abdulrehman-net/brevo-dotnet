namespace Abdul.Brevo.Abstractions.RateLimiting;

/// <summary>
/// Parsed Brevo rate-limit response headers.
/// </summary>
public sealed record BrevoRateLimitInfo
{
    private const string LimitHeader = "x-sib-ratelimit-limit";
    private const string RemainingHeader = "x-sib-ratelimit-remaining";
    private const string ResetHeader = "x-sib-ratelimit-reset";

    /// <summary>Maximum number of requests allowed in the current time window.</summary>
    public int? Limit { get; init; }

    /// <summary>Number of requests remaining in the current time window.</summary>
    public int? Remaining { get; init; }

    /// <summary>Time remaining (in seconds) until the rate-limit counter resets.</summary>
    public int? ResetSeconds { get; init; }

    /// <summary>
    /// Attempts to parse rate-limit information from the given HTTP response headers.
    /// Returns <c>null</c> if none of the expected headers are present.
    /// </summary>
    public static BrevoRateLimitInfo? FromResponse(HttpResponseMessage response)
    {
        var limit = TryGetIntHeader(response, LimitHeader);
        var remaining = TryGetIntHeader(response, RemainingHeader);
        var reset = TryGetIntHeader(response, ResetHeader);

        if (limit is null && remaining is null && reset is null)
            return null;

        return new BrevoRateLimitInfo
        {
            Limit = limit,
            Remaining = remaining,
            ResetSeconds = reset
        };
    }

    private static int? TryGetIntHeader(HttpResponseMessage response, string headerName)
    {
        if (response.Headers.TryGetValues(headerName, out var values))
        {
            var value = values.FirstOrDefault();
            if (int.TryParse(value, out var result))
                return result;
        }

        return null;
    }
}
