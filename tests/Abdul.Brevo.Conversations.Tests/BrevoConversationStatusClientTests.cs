using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Conversations.Tests;

public class BrevoConversationStatusClientTests
{
    private (BrevoConversationStatusClient client, Mock<HttpMessageHandler> handlerMock) CreateClient(HttpStatusCode statusCode, object responseContent)
    {
        var handlerMock = HttpMessageHandlerMock.Setup(statusCode, responseContent);
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };
        var options = Options.Create(new BrevoConversationsOptions { ApiKey = "test-key" });
        var wrapper = new BrevoConversationsHttpClient(httpClient, options);
        var client = new BrevoConversationStatusClient(wrapper);
        return (client, handlerMock);
    }

    [Fact]
    public async Task SetAgentOnlineAsync_SendsPostRequest()
    {
        // Arrange
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, new { });
        var request = new SetBrevoAgentOnlineRequest
        {
            AgentId = "agent-1",
            AgentName = "Agent",
            AgentEmail = "agent@test.com",
            ReceivedFrom = "System"
        };

        // Act
        await client.SetAgentOnlineAsync(request);

        // Assert
        handlerMock.VerifyRequest(HttpMethod.Post, "https://api.brevo.com/v3/conversations/agentOnlinePing", Times.Once());
    }
}
