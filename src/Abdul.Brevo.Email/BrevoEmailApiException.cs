using System.Net;
using Abdul.Brevo.Abstractions.Exceptions;

namespace Abdul.Brevo.Email;

/// <summary>
/// Represents an error returned by the Brevo Email API.
/// </summary>
public sealed class BrevoEmailApiException : BrevoApiException
{
    public BrevoEmailApiException(
        HttpStatusCode statusCode,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(statusCode, responseBody, brevoCode, brevoMessage)
    {
    }

    public BrevoEmailApiException(
        int statusCode,
        string? reasonPhrase,
        string message,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(statusCode, reasonPhrase, message, responseBody, brevoCode, brevoMessage)
    {
    }
}
