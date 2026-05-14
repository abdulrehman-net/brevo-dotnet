using Abdul.Brevo.Abstractions.Exceptions;
using Abdul.Brevo.Abstractions.Http;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Email;

/// <summary>
/// Internal typed HttpClient wrapper for making authenticated requests to the Brevo Email API.
/// </summary>
internal sealed class BrevoEmailHttpClient : BrevoHttpClientBase
{
    public BrevoEmailHttpClient(
        HttpClient httpClient,
        IOptions<BrevoEmailOptions> options)
        : base(httpClient, options.Value)
    {
    }

    /// <inheritdoc />
    protected override BrevoApiException CreateApiException(
        int statusCode,
        string? reasonPhrase,
        string message,
        string? responseBody,
        string? brevoCode,
        string? brevoMessage)
    {
        return new BrevoEmailApiException(
            statusCode,
            reasonPhrase,
            message,
            responseBody,
            brevoCode,
            brevoMessage);
    }
}
