using System.Net;
using Abdul.Brevo.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace Abdul.Brevo.Core.Tests;

public sealed class BrevoAccountClientTests
{
    private static BrevoAccountClient CreateClient(HttpStatusCode statusCode, string jsonResponse)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(jsonResponse)
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };

        var options = Options.Create(new BrevoCoreOptions
        {
            ApiKey = "test-api-key"
        });

        var brevoHttpClient = new BrevoCoreHttpClient(httpClient, options);
        return new BrevoAccountClient(brevoHttpClient);
    }

    [Fact]
    public async Task GetAsync_ReturnsAccount()
    {
        // Arrange
        var client = CreateClient(HttpStatusCode.OK, """
        {
            "email": "test@example.com",
            "companyName": "Test Co",
            "address": {
                "street": "123 Test St",
                "city": "Testville",
                "zipCode": "12345",
                "country": "Testland"
            },
            "plan": [
                {
                    "type": "premium",
                    "creditsType": "Send Limit",
                    "credits": 10000
                }
            ],
            "relay": {
                "enabled": true,
                "port": 587
            },
            "marketingAutomation": {
                "enabled": false
            }
        }
        """);

        // Act
        var result = await client.GetAsync();

        // Assert
        result.Email.Should().Be("test@example.com");
        result.CompanyName.Should().Be("Test Co");
        result.Address.Should().NotBeNull();
        result.Address!.City.Should().Be("Testville");
        result.Plan.Should().HaveCount(1);
        result.Plan![0].Type.Should().Be("premium");
        result.Plan[0].Credits.Should().Be(10000);
        result.Relay.Should().NotBeNull();
        result.Relay!.Enabled.Should().BeTrue();
    }

    [Fact]
    public async Task GetAsync_ThrowsApiException_OnFailure()
    {
        // Arrange
        var client = CreateClient(HttpStatusCode.Unauthorized, """{"code":"unauthorized","message":"Invalid API key"}""");

        // Act
        var act = () => client.GetAsync();

        // Assert
        var ex = await act.Should().ThrowAsync<BrevoCoreApiException>();
        ex.Which.StatusCode.Should().Be(401);
        ex.Which.BrevoCode.Should().Be("unauthorized");
    }
}
