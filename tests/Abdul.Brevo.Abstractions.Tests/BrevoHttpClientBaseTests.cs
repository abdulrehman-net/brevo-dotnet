using System.Net;
using Abdul.Brevo.Abstractions.Exceptions;
using Abdul.Brevo.Abstractions.Http;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace Abdul.Brevo.Abstractions.Tests;

public class BrevoHttpClientBaseTests
{
    private class TestOptions : BrevoOptionsBase { }
    private class TestHttpClient : BrevoHttpClientBase
    {
        public TestHttpClient(HttpClient httpClient, TestOptions options) 
            : base(httpClient, options) { }

        public new Task ThrowIfFailedAsync(HttpResponseMessage response, string responseBody)
            => base.ThrowIfFailedAsync(response, responseBody);
    }

    [Fact]
    public async Task ThrowIfFailedAsync_ShouldThrowRateLimitException_On429()
    {
        // Arrange
        var handler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(handler.Object);
        var options = new TestOptions { ApiKey = "test-key" };
        var client = new TestHttpClient(httpClient, options);

        var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
        response.Headers.Add("x-sib-ratelimit-reset", "5");
        var responseBody = "{\"code\":\"too_many_requests\",\"message\":\"Rate limit exceeded\"}";

        // Act
        var act = () => client.ThrowIfFailedAsync(response, responseBody);

        // Assert
        var ex = await act.Should().ThrowAsync<BrevoRateLimitException>();
        ex.Which.RetryAfter.Should().Be(TimeSpan.FromSeconds(5));
        ex.Which.BrevoCode.Should().Be("too_many_requests");
    }

    [Fact]
    public async Task ThrowIfFailedAsync_ShouldThrowPaymentRequiredException_On402()
    {
        // Arrange
        var handler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(handler.Object);
        var options = new TestOptions { ApiKey = "test-key" };
        var client = new TestHttpClient(httpClient, options);

        var response = new HttpResponseMessage(HttpStatusCode.PaymentRequired);
        var responseBody = "{\"code\":\"not_enough_credits\",\"message\":\"Credits exhausted\"}";

        // Act
        var act = () => client.ThrowIfFailedAsync(response, responseBody);

        // Assert
        var ex = await act.Should().ThrowAsync<BrevoPaymentRequiredException>();
        ex.Which.BrevoCode.Should().Be("not_enough_credits");
    }
}
