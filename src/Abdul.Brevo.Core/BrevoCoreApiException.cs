using System.Net;
using Abdul.Brevo.Abstractions.Exceptions;

namespace Abdul.Brevo.Core;

/// <summary>
/// Represents an error returned by the Brevo Core API.
/// </summary>
public sealed class BrevoCoreApiException : BrevoApiException
{
    public BrevoCoreApiException(
        HttpStatusCode statusCode,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(statusCode, responseBody, brevoCode, brevoMessage)
    {
    }

    public BrevoCoreApiException(
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
