using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Email.Tests;

public sealed class BrevoScheduledEmailClientTests
{
    private static BrevoScheduledEmailClient CreateClient(
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
        return new BrevoScheduledEmailClient(brevoHttpClient);
    }

    [Fact]
    public async Task GetScheduledAsync_ReturnsScheduledInfo()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """{"count":1,"batches":[{"status":"queued","scheduledAt":"2026-06-01T10:00:00Z"}]}""");

        var client = CreateClient(handler);

        var result = await client.GetScheduledAsync("batch-123");

        result.Count.Should().Be(1);
        result.Batches.Should().HaveCount(1);
        result.Batches![0].Status.Should().Be("queued");
    }

    [Fact]
    public async Task GetScheduledAsync_ThrowsOnEmptyIdentifier()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentException>(
            () => client.GetScheduledAsync(""));
    }

    [Fact]
    public async Task DeleteScheduledAsync_Succeeds()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.NoContent, "");
        var client = CreateClient(handler);

        await client.DeleteScheduledAsync("batch-123");
        // No exception = success
    }

    [Fact]
    public async Task DeleteScheduledAsync_ThrowsOnEmptyIdentifier()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentException>(
            () => client.DeleteScheduledAsync(""));
    }

    [Fact]
    public async Task DeleteScheduledAsync_ThrowsOnApiError()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.NotFound,
            """{"message":"Not found"}""");

        var client = CreateClient(handler);

        await Assert.ThrowsAsync<BrevoEmailApiException>(
            () => client.DeleteScheduledAsync("nonexistent"));
    }
}
