using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Moq;
using Moq.Protected;

namespace Abdul.Brevo.Conversations.Tests;

public static class HttpMessageHandlerMock
{
    public static Mock<HttpMessageHandler> Setup(
        HttpStatusCode statusCode,
        object? responseContent = null,
        string? stringContent = null)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        
        var response = new HttpResponseMessage(statusCode);
        
        if (responseContent != null)
        {
            var json = JsonSerializer.Serialize(responseContent, new JsonSerializerOptions(JsonSerializerDefaults.Web) 
            { 
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
            });
            response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
        else if (stringContent != null)
        {
            response.Content = new StringContent(stringContent, System.Text.Encoding.UTF8, "application/json");
        }

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(response);

        return handlerMock;
    }

    public static void VerifyRequest(
        this Mock<HttpMessageHandler> handlerMock,
        HttpMethod method,
        string path,
        Times times)
    {
        handlerMock.Protected().Verify(
            "SendAsync",
            times,
            ItExpr.Is<HttpRequestMessage>(req => 
                req.Method == method && 
                req.RequestUri != null && 
                req.RequestUri.ToString() == path),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}
