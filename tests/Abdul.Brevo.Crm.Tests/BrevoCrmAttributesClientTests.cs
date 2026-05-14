using System.Net;
using Abdul.Brevo.Crm.Models.Attributes;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmAttributesClientTests
{
    private static BrevoCrmAttributesClient CreateClient(
        HttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.brevo.com")
        };

        var options = Options.Create(new BrevoCrmOptions
        {
            ApiKey = "test-api-key"
        });

        var brevoHttpClient = new BrevoCrmHttpClient(httpClient, options);
        return new BrevoCrmAttributesClient(brevoHttpClient);
    }

    [Fact]
    public async Task ListCompanyAttributesAsync_ReturnsAttributes()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """[{"internalName":"domain","label":"Domain","attributeTypeName":"text"}]""");

        var client = CreateClient(handler);

        var result = await client.ListCompanyAttributesAsync();

        result.Should().HaveCount(1);
        result[0].InternalName.Should().Be("domain");
    }

    [Fact]
    public async Task CreateAsync_SendsPostRequest()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });

        var client = CreateClient(handler);

        await client.CreateAsync(new CreateCrmAttributeRequest
        {
            Label = "Custom",
            AttributeType = "text",
            ObjectType = "deals"
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.Method.Should().Be(HttpMethod.Post);
        capturedRequest.RequestUri!.PathAndQuery.Should().Be("/v3/crm/attributes");
    }
}
