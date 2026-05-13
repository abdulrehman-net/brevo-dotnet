using System.Net;
using Abdul.Brevo.Email.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Email.Tests;

public sealed class BrevoTransactionalEmailClientTests
{
    private static BrevoTransactionalEmailClient CreateClient(
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
        return new BrevoTransactionalEmailClient(brevoHttpClient);
    }

    [Fact]
    public async Task SendAsync_ReturnsMessageId()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.Created,
            """{"messageId":"<test-msg-id@relay.domain.com>"}""");

        var client = CreateClient(handler);

        var result = await client.SendAsync(new SendTransactionalEmailRequest
        {
            Sender = new EmailSender { Email = "test@example.com", Name = "Test" },
            To = [new EmailRecipient { Email = "to@example.com" }],
            Subject = "Test",
            HtmlContent = "<p>Hello</p>"
        });

        result.MessageId.Should().Be("<test-msg-id@relay.domain.com>");
    }

    [Fact]
    public async Task SendAsync_ThrowsOnNull()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => client.SendAsync(null!));
    }

    [Fact]
    public async Task SendAsync_ThrowsOnApiError()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.BadRequest,
            """{"message":"Invalid request"}""");

        var client = CreateClient(handler);

        await Assert.ThrowsAsync<BrevoEmailApiException>(
            () => client.SendAsync(new SendTransactionalEmailRequest
            {
                Sender = new EmailSender { Email = "test@example.com" },
                To = [new EmailRecipient { Email = "to@example.com" }],
                Subject = "Test",
                HtmlContent = "<p>Hello</p>"
            }));
    }

    [Fact]
    public async Task ListAsync_ReturnsList()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """{"count":1,"transactionalEmails":[{"messageId":"msg-1","email":"to@example.com"}]}""");

        var client = CreateClient(handler);

        var result = await client.ListAsync();

        result.Count.Should().Be(1);
        result.TransactionalEmails.Should().HaveCount(1);
        result.TransactionalEmails![0].MessageId.Should().Be("msg-1");
    }

    [Fact]
    public async Task ListAsync_WithFilters_BuildsQueryString()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"count":0,"transactionalEmails":[]}""")
            });
        });

        var client = CreateClient(handler);

        await client.ListAsync(new ListTransactionalEmailsRequest
        {
            Email = "test@example.com",
            Limit = 10
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.RequestUri!.Query.Should().Contain("email=test%40example.com");
        capturedRequest.RequestUri.Query.Should().Contain("limit=10");
    }

    [Fact]
    public async Task GetContentAsync_ReturnsContent()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """{"messageId":"msg-1","subject":"Test","htmlContent":"<p>Hello</p>"}""");

        var client = CreateClient(handler);

        var result = await client.GetContentAsync("msg-1");

        result.MessageId.Should().Be("msg-1");
        result.Subject.Should().Be("Test");
        result.HtmlContent.Should().Be("<p>Hello</p>");
    }

    [Fact]
    public async Task GetContentAsync_ThrowsOnEmptyId()
    {
        var handler = new HttpMessageHandlerMock(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentException>(
            () => client.GetContentAsync(""));
    }
}
