using Abdul.Brevo.Abstractions.Exceptions;
using Abdul.Brevo.Abstractions.Http;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Core;

/// <summary>
/// Internal typed HttpClient wrapper for making authenticated requests to the Brevo Core API.
/// </summary>
internal sealed class BrevoCoreHttpClient : BrevoHttpClientBase
{
    public BrevoCoreHttpClient(
        HttpClient httpClient,
        IOptions<BrevoCoreOptions> options)
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
        return new BrevoCoreApiException(
            statusCode,
            reasonPhrase,
            message,
            responseBody,
            brevoCode,
            brevoMessage);
    }
}
