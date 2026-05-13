using System.Net;

namespace Abdul.Brevo.Conversations;

/// <summary>
/// Thrown when a Brevo Conversations API request fails.
/// </summary>
public class BrevoConversationsApiException : Exception
{
    /// <summary>Gets the HTTP status code returned by Brevo.</summary>
    public int StatusCode { get; }

    /// <summary>Gets the HTTP reason phrase returned by Brevo.</summary>
    public string? ReasonPhrase { get; }

    /// <summary>Gets the raw response body returned by Brevo.</summary>
    public string? ResponseBody { get; }

    public BrevoConversationsApiException(
        int statusCode,
        string? reasonPhrase,
        string message,
        string? responseBody)
        : base(message)
    {
        StatusCode = statusCode;
        ReasonPhrase = reasonPhrase;
        ResponseBody = responseBody;
    }

    internal BrevoConversationsApiException(
        HttpStatusCode statusCode,
        string? responseBody)
        : base(GetLegacyErrorMessage(statusCode, responseBody))
    {
        StatusCode = (int)statusCode;
        ReasonPhrase = statusCode.ToString();
        ResponseBody = responseBody;
    }

    private static string GetLegacyErrorMessage(HttpStatusCode statusCode, string? responseBody)
    {
        var baseMessage = $"Brevo Conversations API request failed with status code {(int)statusCode} ({statusCode}).";

        var hint = statusCode switch
        {
            HttpStatusCode.Unauthorized => " Check that your API key is valid and configured correctly.",
            HttpStatusCode.PaymentRequired => " Your Brevo account plan does not include access to the Conversations API or you have exceeded your limits. Please check your subscription at https://app.brevo.com",
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

