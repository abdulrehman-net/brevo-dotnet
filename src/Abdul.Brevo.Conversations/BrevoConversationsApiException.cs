using System.Net;
using Abdul.Brevo.Abstractions.Exceptions;

namespace Abdul.Brevo.Conversations;

/// <summary>
/// Thrown when a Brevo Conversations API request fails.
/// </summary>
public class BrevoConversationsApiException : BrevoApiException
{
    public BrevoConversationsApiException(
        int statusCode,
        string? reasonPhrase,
        string message,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(statusCode, reasonPhrase, message, responseBody, brevoCode, brevoMessage)
    {
    }

    internal BrevoConversationsApiException(
        HttpStatusCode statusCode,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(statusCode, responseBody, brevoCode, brevoMessage)
    {
    }
}
