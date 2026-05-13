using System.Net;

namespace Nexin.Brevo.Conversations;

public sealed class BrevoConversationsApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string? ResponseBody { get; }

    public BrevoConversationsApiException(
        HttpStatusCode statusCode,
        string? responseBody)
        : base($"Brevo Conversations API request failed with status code {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}
