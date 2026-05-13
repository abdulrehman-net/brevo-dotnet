using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Conversations.Tests;

public class BrevoConversationsHttpClientTests
{
    private BrevoConversationsHttpClient CreateClient(Mock<HttpMessageHandler> handlerMock)
    {
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };
        var options = Options.Create(new BrevoConversationsOptions { ApiKey = "test-key" });
        return new BrevoConversationsHttpClient(httpClient, options);
    }

    [Fact]
    public async Task GetAsync_WhenErrorResponse_ThrowsApiException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(HttpStatusCode.BadRequest, stringContent: "{\"error\": \"bad request\"}");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.GetAsync<object>("/test");

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsApiException>();
        exception.Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        exception.Which.ResponseBody.Should().Be("{\"error\": \"bad request\"}");
    }

    [Fact]
    public async Task PostAsync_WhenErrorResponse_ThrowsApiException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(HttpStatusCode.InternalServerError, stringContent: "error");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.PostAsync<object, object>("/test", new { });

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsApiException>();
        exception.Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task PutAsync_WhenErrorResponse_ThrowsApiException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(HttpStatusCode.NotFound, stringContent: "error");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.PutAsync<object, object>("/test", new { });

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsApiException>();
        exception.Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteAsync_WhenErrorResponse_ThrowsApiException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(HttpStatusCode.Forbidden, stringContent: "error");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.DeleteAsync("/test");

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsApiException>();
        exception.Which.StatusCode.Should().Be((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task PostNoContentAsync_WhenErrorResponse_ThrowsApiException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(HttpStatusCode.Unauthorized, stringContent: "error");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.PostNoContentAsync("/test", new { });

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsApiException>();
        exception.Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetAsync_WhenNullResponse_ThrowsApiException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(HttpStatusCode.OK, stringContent: "null");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.GetAsync<object>("/test");

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsApiException>();
        exception.Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task SendAsync_WhenPaymentRequired_ThrowsPaymentRequiredException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(
            HttpStatusCode.PaymentRequired,
            stringContent: "{\"code\":\"not_enough_credits\",\"message\":\"Upgrade your plan to use REST API\"}");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.GetAsync<object>("/test");

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsPaymentRequiredException>();
        exception.Which.StatusCode.Should().Be(402);
        exception.Which.BrevoCode.Should().Be("not_enough_credits");
        exception.Which.Message.Should().Be("Upgrade your plan to use REST API");
    }

    [Fact]
    public async Task PostNoContentAsync_WhenPaymentRequired_ThrowsPaymentRequiredException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(
            HttpStatusCode.PaymentRequired,
            stringContent: "{\"code\":\"not_enough_credits\",\"message\":\"Upgrade your plan to use REST API\"}");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.PostNoContentAsync("/test", new { });

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsPaymentRequiredException>();
        exception.Which.StatusCode.Should().Be(402);
        exception.Which.BrevoCode.Should().Be("not_enough_credits");
    }

    [Fact]
    public async Task DeleteAsync_WhenPaymentRequired_ThrowsPaymentRequiredException()
    {
        // Arrange
        var handlerMock = HttpMessageHandlerMock.Setup(
            HttpStatusCode.PaymentRequired,
            stringContent: "{\"code\":\"not_enough_credits\",\"message\":\"Upgrade your plan to use REST API\"}");
        var client = CreateClient(handlerMock);

        // Act
        var act = () => client.DeleteAsync("/test");

        // Assert
        var exception = await act.Should().ThrowAsync<BrevoConversationsPaymentRequiredException>();
        exception.Which.StatusCode.Should().Be(402);
        exception.Which.BrevoCode.Should().Be("not_enough_credits");
    }
}
