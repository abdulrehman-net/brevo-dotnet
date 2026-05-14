using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmPipelineClientTests
{
    private static BrevoCrmPipelineClient CreateClient(
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
        return new BrevoCrmPipelineClient(brevoHttpClient);
    }

    [Fact]
    public async Task ListAsync_ReturnsPipelines()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """[{"pipelineId":"p1","pipelineName":"Sales","stages":[{"id":"s1","name":"Qualify"}]}]""");

        var client = CreateClient(handler);

        var result = await client.ListAsync();

        result.Should().HaveCount(1);
        result[0].PipelineId.Should().Be("p1");
        result[0].Stages.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAsync_ReturnsPipeline()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """{"pipelineId":"p1","pipelineName":"Sales"}""");

        var client = CreateClient(handler);

        var result = await client.GetAsync("p1");

        result.PipelineId.Should().Be("p1");
    }
}
