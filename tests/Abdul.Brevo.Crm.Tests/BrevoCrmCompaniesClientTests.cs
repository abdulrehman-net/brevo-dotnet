using System.Net;
using Abdul.Brevo.Crm.Models.Companies;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmCompaniesClientTests
{
    private static BrevoCrmCompaniesClient CreateClient(
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
        return new BrevoCrmCompaniesClient(brevoHttpClient);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCompany()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.Created,
            """{"id":"comp-1","attributes":{"name":"Test Company"}}""");

        var client = CreateClient(handler);

        var result = await client.CreateAsync(new CreateCompanyRequest
        {
            Name = "Test Company"
        });

        result.Id.Should().Be("comp-1");
        result.Attributes!["name"].ToString().Should().Be("Test Company");
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
                Content = new StringContent("""{"count":0,"companies":[]}""")
            });
        });

        var client = CreateClient(handler);

        await client.ListAsync(new ListCompaniesRequest
        {
            FiltersAttributesName = "Test",
            Limit = 10
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.RequestUri!.Query.Should().Contain("filters[attributes.name]=Test");
        capturedRequest.RequestUri.Query.Should().Contain("limit=10");
    }

    [Fact]
    public async Task UpdateAsync_SendsPatchRequest()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });

        var client = CreateClient(handler);

        await client.UpdateAsync("comp-1", new UpdateCompanyRequest
        {
            Name = "New Name"
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.Method.ToString().Should().Be("PATCH");
        capturedRequest.RequestUri!.PathAndQuery.Should().Be("/v3/companies/comp-1");
    }

    [Fact]
    public async Task DeleteAsync_SendsDeleteRequest()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });

        var client = CreateClient(handler);

        await client.DeleteAsync("comp-1");

        capturedRequest.Should().NotBeNull();
        capturedRequest!.Method.Should().Be(HttpMethod.Delete);
    }
}
