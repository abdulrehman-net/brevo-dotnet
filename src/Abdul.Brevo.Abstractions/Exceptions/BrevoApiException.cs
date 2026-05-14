using System.Net;

namespace Abdul.Brevo.Abstractions.Exceptions;

/// <summary>
/// Base exception for all Brevo API errors. Provides structured access to the
/// HTTP status code, the raw response body, and the Brevo-specific error code
/// and message parsed from the JSON error body.
/// </summary>
public class BrevoApiException : Exception
{
    /// <summary>Gets the HTTP status code returned by Brevo.</summary>
    public int StatusCode { get; }

    /// <summary>Gets the HTTP reason phrase returned by Brevo.</summary>
    public string? ReasonPhrase { get; }

    /// <summary>Gets the raw response body returned by Brevo.</summary>
    public string? ResponseBody { get; }

    /// <summary>
    /// Gets the machine-readable Brevo error code (e.g. <c>invalid_parameter</c>,
    /// <c>unauthorized</c>, <c>not_enough_credits</c>).
    /// </summary>
    public string? BrevoCode { get; }

    /// <summary>
    /// Gets the human-readable error message returned by the Brevo API.
    /// </summary>
    public string? BrevoMessage { get; }

    public BrevoApiException(
        int statusCode,
        string? reasonPhrase,
        string message,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(message)
    {
        StatusCode = statusCode;
        ReasonPhrase = reasonPhrase;
        ResponseBody = responseBody;
        BrevoCode = brevoCode;
        BrevoMessage = brevoMessage;
    }

    public BrevoApiException(
        HttpStatusCode statusCode,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(BuildMessage(statusCode, responseBody, brevoMessage))
    {
        StatusCode = (int)statusCode;
        ReasonPhrase = statusCode.ToString();
        ResponseBody = responseBody;
        BrevoCode = brevoCode;
        BrevoMessage = brevoMessage;
    }

    private static string BuildMessage(
        HttpStatusCode statusCode,
        string? responseBody,
        string? brevoMessage)
    {
        var baseMsg = brevoMessage
                      ?? $"Brevo API request failed with status code {(int)statusCode} ({statusCode}).";

        var hint = statusCode switch
        {
            HttpStatusCode.Unauthorized =>
                " Check that your API key is valid and configured correctly.",
            HttpStatusCode.PaymentRequired =>
                " Your Brevo account plan may not include this feature or you have exceeded your limits.",
            HttpStatusCode.Forbidden =>
                " Check that your IP address is authorized in your Brevo account settings.",
            HttpStatusCode.NotFound =>
                " The requested resource was not found.",
            HttpStatusCode.TooManyRequests =>
                " Rate limit exceeded. Please wait before making more requests.",
            _ => string.Empty
        };

        return !string.IsNullOrWhiteSpace(responseBody)
            ? $"{baseMsg}{hint} Response: {responseBody}"
            : $"{baseMsg}{hint}";
    }
}
