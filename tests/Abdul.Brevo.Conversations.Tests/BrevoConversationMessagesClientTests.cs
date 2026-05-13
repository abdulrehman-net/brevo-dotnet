using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Conversations.Tests;

public class BrevoConversationMessagesClientTests
{
    private (BrevoConversationMessagesClient client, Mock<HttpMessageHandler> handlerMock) CreateClient(HttpStatusCode statusCode, object responseContent)
    {
        var handlerMock = HttpMessageHandlerMock.Setup(statusCode, responseContent);
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };
        var options = Options.Create(new BrevoConversationsOptions { ApiKey = "test-key" });
        var wrapper = new BrevoConversationsHttpClient(httpClient, options);
        var client = new BrevoConversationMessagesClient(wrapper);
        return (client, handlerMock);
    }

    [Fact]
    public async Task GetMessageAsync_ValidId_ReturnsMessage()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Hello" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);

        // Act
        var result = await client.GetMessageAsync("msg-123");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("msg-123");
        result.Text.Should().Be("Hello");
        handlerMock.VerifyRequest(HttpMethod.Get, "https://api.brevo.com/v3/conversations/messages/msg-123", Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetMessageAsync_InvalidId_ThrowsArgumentException(string id)
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });

        // Act
        var act = () => client.GetMessageAsync(id!);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task DeleteAgentMessageAsync_ValidId_SendsDeleteRequest()
    {
        // Arrange
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, new { });

        // Act
        await client.DeleteAgentMessageAsync("msg-123");

        // Assert
        handlerMock.VerifyRequest(HttpMethod.Delete, "https://api.brevo.com/v3/conversations/messages/msg-123", Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task DeleteAgentMessageAsync_InvalidId_ThrowsArgumentException(string id)
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });

        // Act
        var act = () => client.DeleteAgentMessageAsync(id!);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task SendAgentMessageAsync_WithAgentId_SendsPostRequest()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Hello" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);
        var request = new SendBrevoAgentMessageRequest
        {
            AgentId = "agent-1",
            Text = "Hello",
            VisitorId = "visitor-1"
        };

        // Act
        var result = await client.SendAgentMessageAsync(request);

        // Assert
        result.Id.Should().Be("msg-123");
        handlerMock.VerifyRequest(HttpMethod.Post, "https://api.brevo.com/v3/conversations/messages", Times.Once());
    }

    [Fact]
    public async Task SendAgentMessageAsync_WithExternalAgent_SendsPostRequest()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Hello" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);
        var request = new SendBrevoAgentMessageRequest
        {
            AgentEmail = "agent@test.com",
            AgentName = "Agent",
            ReceivedFrom = "Helpdesk",
            Text = "Hello",
            VisitorId = "visitor-1"
        };

        // Act
        var result = await client.SendAgentMessageAsync(request);

        // Assert
        result.Id.Should().Be("msg-123");
        handlerMock.VerifyRequest(HttpMethod.Post, "https://api.brevo.com/v3/conversations/messages", Times.Once());
    }

    [Fact]
    public async Task SendAgentMessageAsync_MissingIdentity_ThrowsArgumentException()
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });
        var request = new SendBrevoAgentMessageRequest
        {
            Text = "Hello",
            VisitorId = "visitor-1"
        };

        // Act
        var act = () => client.SendAgentMessageAsync(request);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Provide either agentId, or all three values: agentEmail, agentName, and receivedFrom.");
    }

    [Fact]
    public async Task UpdateAgentMessageAsync_ValidId_SendsPutRequest()
    {
        // Arrange
        var expectedMessage = new BrevoConversationMessage { Id = "msg-123", Text = "Updated" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedMessage);
        var request = new UpdateBrevoMessageRequest { Text = "Updated" };

        // Act
        var result = await client.UpdateAgentMessageAsync("msg-123", request);

        // Assert
        result.Id.Should().Be("msg-123");
        result.Text.Should().Be("Updated");
        handlerMock.VerifyRequest(HttpMethod.Put, "https://api.brevo.com/v3/conversations/messages/msg-123", Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task UpdateAgentMessageAsync_InvalidId_ThrowsArgumentException(string id)
    {
        // Arrange
        var (client, _) = CreateClient(HttpStatusCode.OK, new { });
        var request = new UpdateBrevoMessageRequest { Text = "Updated" };

        // Act
        var act = () => client.UpdateAgentMessageAsync(id!, request);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
}
