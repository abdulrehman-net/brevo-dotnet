using System.Net;
using System.Text.Json;
using Abdul.Brevo.Abstractions.Exceptions;
using Abdul.Brevo.Abstractions.Http;
using Abdul.Brevo.Abstractions.RateLimiting;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Conversations;

/// <summary>
/// Internal typed HttpClient wrapper for making authenticated requests
/// to the Brevo Conversations API. Overrides error handling to raise
/// domain-specific exceptions.
/// </summary>
internal sealed class BrevoConversationsHttpClient : BrevoHttpClientBase
{
    public BrevoConversationsHttpClient(
        HttpClient httpClient,
        IOptions<BrevoConversationsOptions> options)
        : base(httpClient, options.Value)
    {
    }

    /// <inheritdoc />
    protected override Task ThrowIfFailedAsync(
        HttpResponseMessage response,
        string responseBody)
    {
        if (response.StatusCode == HttpStatusCode.PaymentRequired)
        {
            BrevoErrorResponse? brevoError = null;
            try
            {
                brevoError = JsonSerializer.Deserialize<BrevoErrorResponse>(responseBody, JsonOptions);
            }
            catch { }

            throw new BrevoConversationsPaymentRequiredException(
                message: brevoError?.Message
                         ?? "Brevo returned 402 Payment Required. The Conversations REST API may require a paid plan, enabled feature access, or available credits.",
                brevoCode: brevoError?.Code,
                responseBody: responseBody);
        }

        return base.ThrowIfFailedAsync(response, responseBody);
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
        return new BrevoConversationsApiException(
            statusCode,
            reasonPhrase,
            message,
            responseBody,
            brevoCode,
            brevoMessage);
    }
}
