using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Conversations.Tests;

public class BrevoConversationVisitorsClientTests
{
    private (BrevoConversationVisitorsClient client, Mock<HttpMessageHandler> handlerMock) CreateClient(HttpStatusCode statusCode, object responseContent)
    {
        var handlerMock = HttpMessageHandlerMock.Setup(statusCode, responseContent);
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };
        var options = Options.Create(new BrevoConversationsOptions { ApiKey = "test-key" });
        var wrapper = new BrevoConversationsHttpClient(httpClient, options);
        var client = new BrevoConversationVisitorsClient(wrapper);
        return (client, handlerMock);
    }

    [Fact]
    public async Task SetVisitorGroupAsync_SendsPutRequest()
    {
        // Arrange
        var expectedResponse = new BrevoVisitorGroupAssignmentResponse { GroupId = "group-1", VisitorId = "visitor-1" };
        var (client, handlerMock) = CreateClient(HttpStatusCode.OK, expectedResponse);
        var request = new SetBrevoVisitorGroupRequest
        {
            GroupId = "group-1"
        };

        // Act
        var result = await client.SetVisitorGroupAsync(request);

        // Assert
        result.GroupId.Should().Be("group-1");
        result.VisitorId.Should().Be("visitor-1");
        handlerMock.VerifyRequest(HttpMethod.Put, "https://api.brevo.com/v3/conversations/visitorGroup", Times.Once());
    }
}
