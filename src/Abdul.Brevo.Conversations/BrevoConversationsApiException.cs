using System.Net;

namespace Abdul.Brevo.Conversations;

public sealed class BrevoConversationsApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string? ResponseBody { get; }

    public BrevoConversationsApiException(
        HttpStatusCode statusCode,
        string? responseBody)
        : base(GetErrorMessage(statusCode, responseBody))
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    private static string GetErrorMessage(HttpStatusCode statusCode, string? responseBody)
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
