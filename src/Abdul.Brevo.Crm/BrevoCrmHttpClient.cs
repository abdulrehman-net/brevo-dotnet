using Abdul.Brevo.Abstractions.Exceptions;
using Abdul.Brevo.Abstractions.Http;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Internal typed HttpClient wrapper for making authenticated requests to the Brevo CRM API.
/// </summary>
internal sealed class BrevoCrmHttpClient : BrevoHttpClientBase
{
    public BrevoCrmHttpClient(
        HttpClient httpClient,
        IOptions<BrevoCrmOptions> options)
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
        return new BrevoCrmApiException(
            statusCode,
            reasonPhrase,
            message,
            responseBody,
            brevoCode,
            brevoMessage);
    }
}
