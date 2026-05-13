using System.Net;
using Abdul.Brevo.Email.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Email.Tests;

public sealed class BrevoHardBounceClientTests
{
    private static BrevoHardBounceClient CreateClient(
        HttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };

        var options = Options.Create(new BrevoEmailOptions
        {
            ApiKey = "test-api-key"
        });

        var brevoHttpClient = new BrevoEmailHttpClient(httpClient, options);
        return new BrevoHardBounceClient(brevoHttpClient);
    }

    [Fact]
    public async Task DeleteAsync_Succeeds()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.NoContent, "");
        var client = CreateClient(handler);

        await client.DeleteAsync(new DeleteHardBouncesRequest
        {
            StartDate = "2026-01-01",
            EndDate = "2026-01-31"
        });
        // No exception = success
    }

    [Fact]
    public async Task DeleteAsync_ThrowsOnNull()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => client.DeleteAsync(null!));
    }

    [Fact]
    public async Task DeleteAsync_ThrowsOnApiError()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.BadRequest,
            """{"message":"Invalid date range"}""");

        var client = CreateClient(handler);

        await Assert.ThrowsAsync<BrevoEmailApiException>(
            () => client.DeleteAsync(new DeleteHardBouncesRequest
            {
                StartDate = "invalid"
            }));
    }

    [Fact]
    public async Task DeleteAsync_SendsToCorrectEndpoint()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });

        var client = CreateClient(handler);

        await client.DeleteAsync(new DeleteHardBouncesRequest
        {
            ContactEmail = "bounce@example.com"
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.RequestUri!.PathAndQuery.Should().Be("/v3/smtp/deleteHardbounces");
        capturedRequest.Method.Should().Be(HttpMethod.Post);
    }
}
