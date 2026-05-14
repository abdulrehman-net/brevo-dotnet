using System.Net;
using Abdul.Brevo.Crm.Models.Deals;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmDealsClientTests
{
    private static BrevoCrmDealsClient CreateClient(
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
        return new BrevoCrmDealsClient(brevoHttpClient);
    }

    [Fact]
    public async Task CreateAsync_ReturnsDeal()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.Created,
            """{"id":"deal-1","attributes":{"deal_name":"Test Deal"}}""");

        var client = CreateClient(handler);

        var result = await client.CreateAsync(new CreateDealRequest
        {
            Name = "Test Deal"
        });

        result.Id.Should().Be("deal-1");
        result.Attributes!["deal_name"].ToString().Should().Be("Test Deal");
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
                Content = new StringContent("""{"count":0,"deals":[]}""")
            });
        });

        var client = CreateClient(handler);

        await client.ListAsync(new ListDealsRequest
        {
            FiltersAttributesDealName = "Test",
            Offset = 50
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.RequestUri!.Query.Should().Contain("filters[attributes.deal_name]=Test");
        capturedRequest.RequestUri.Query.Should().Contain("offset=50");
    }

    [Fact]
    public async Task LinkUnlinkAsync_SendsPatchRequest()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });

        var client = CreateClient(handler);

        await client.LinkUnlinkAsync("deal-1", new DealLinkUnlinkRequest
        {
            LinkContactIds = [1, 2]
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.Method.ToString().Should().Be("PATCH");
        capturedRequest.RequestUri!.PathAndQuery.Should().Be("/v3/crm/deals/link-unlink/deal-1");
    }
}
