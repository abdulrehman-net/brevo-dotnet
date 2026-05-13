using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Conversations.Tests;

public class BrevoAutomatedMessagesClientTests
{
    private (BrevoAutomatedMessagesClient client, Mock<HttpMessageHandler> handlerMock) CreateClient(HttpStatusCode statusCode, object responseContent)
    {
        var handlerMock = HttpMessageHandlerMock.Setup(statusCode, responseContent);
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };
        var options = Options.Create(new BrevoConversationsOptions { ApiKey = "test-key" });
        var wrapper = new BrevoConversationsHttpClient(httpClient, options);
        var client = new BrevoAutomatedMessagesClient(wrapper);
        return (client, handlerMock);
    }

    [Fact]
    public async Task SendAutomatedMessageAsync_SendsPostRequest()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Hello Auto" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);
        var request = new SendBrevoAutomatedMessageRequest
        {
            Text = "Hello Auto",
            VisitorId = "visitor-1",
            AgentId = "agent-1",
            GroupId = "group-1"
        };

        // Act
        var result = await client.SendAutomatedMessageAsync(request);

        // Assert
        result.Id.Should().Be("msg-123");
        handlerMock.VerifyRequest(HttpMethod.Post, "https://api.brevo.com/v3/conversations/pushedMessages", Times.Once());
    }

    [Fact]
    public async Task GetAutomatedMessageAsync_ValidId_ReturnsMessage()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Hello" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);

        // Act
        var result = await client.GetAutomatedMessageAsync("msg-123");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("msg-123");
        result.Text.Should().Be("Hello");
        handlerMock.VerifyRequest(HttpMethod.Get, "https://api.brevo.com/v3/conversations/pushedMessages/msg-123", Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetAutomatedMessageAsync_InvalidId_ThrowsArgumentException(string id)
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });

        // Act
        var act = () => client.GetAutomatedMessageAsync(id!);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdateAutomatedMessageAsync_ValidId_SendsPutRequest()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Updated" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);
        var request = new UpdateBrevoMessageRequest { Text = "Updated" };

        // Act
        var result = await client.UpdateAutomatedMessageAsync("msg-123", request);

        // Assert
        result.Id.Should().Be("msg-123");
        result.Text.Should().Be("Updated");
        handlerMock.VerifyRequest(HttpMethod.Put, "https://api.brevo.com/v3/conversations/pushedMessages/msg-123", Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task UpdateAutomatedMessageAsync_InvalidId_ThrowsArgumentException(string id)
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });
        var request = new UpdateBrevoMessageRequest { Text = "Updated" };

        // Act
        var act = () => client.UpdateAutomatedMessageAsync(id!, request);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task DeleteAutomatedMessageAsync_ValidId_SendsDeleteRequest()
    {
        // Arrange
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, new { });

        // Act
        await client.DeleteAutomatedMessageAsync("msg-123");

        // Assert
        handlerMock.VerifyRequest(HttpMethod.Delete, "https://api.brevo.com/v3/conversations/pushedMessages/msg-123", Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task DeleteAutomatedMessageAsync_InvalidId_ThrowsArgumentException(string id)
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });

        // Act
        var act = () => client.DeleteAutomatedMessageAsync(id!);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
}
