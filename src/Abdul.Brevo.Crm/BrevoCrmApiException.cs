using System.Net;
using Abdul.Brevo.Abstractions.Exceptions;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Represents an error returned by the Brevo Sales CRM API.
/// </summary>
public sealed class BrevoCrmApiException : BrevoApiException
{
    public BrevoCrmApiException(
        HttpStatusCode statusCode,
        string? responseBody,
        string? brevoCode = null,
        string? brevoMessage = null)
        : base(statusCode, responseBody, brevoCode, brevoMessage)
    {
    }

    public BrevoCrmApiException(
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
