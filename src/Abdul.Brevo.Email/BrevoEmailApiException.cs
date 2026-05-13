using System.Net;

namespace Abdul.Brevo.Email;

/// <summary>
/// Represents an error returned by the Brevo Email API.
/// </summary>
public sealed class BrevoEmailApiException : Exception
{
    /// <summary>
    /// The HTTP status code returned by the API.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// The raw response body returned by the API, if available.
    /// </summary>
    public string? ResponseBody { get; }

    public BrevoEmailApiException(
        HttpStatusCode statusCode,
        string? responseBody)
        : base(GetErrorMessage(statusCode, responseBody))
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    private static string GetErrorMessage(HttpStatusCode statusCode, string? responseBody)
    {
        var baseMessage = $"Brevo Email API request failed with status code {(int)statusCode} ({statusCode}).";

        var hint = statusCode switch
        {
            HttpStatusCode.Unauthorized => " Check that your API key is valid and configured correctly.",
            HttpStatusCode.PaymentRequired => " Your Brevo account plan may not include transactional email or you have exceeded your limits.",
            HttpStatusCode.Forbidden => " Check that your IP address is authorized in your Brevo account settings.",
            HttpStatusCode.NotFound => " The requested resource was not found.",
            HttpStatusCode.TooManyRequests => " Rate limit exceeded. Please wait before making more requests.",
            _ => string.Empty
        };

        return !string.IsNullOrWhiteSpace(responseBody)
            ? $"{baseMessage}{hint} Response: {responseBody}"
            : $"{baseMessage}{hint}";
    }
}
