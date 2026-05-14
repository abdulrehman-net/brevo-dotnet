using System.Net;
using Abdul.Brevo.Crm.Models.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmTasksClientTests
{
    private static BrevoCrmTasksClient CreateClient(
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
        return new BrevoCrmTasksClient(brevoHttpClient);
    }

    [Fact]
    public async Task CreateAsync_ReturnsTask()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.Created,
            """{"id":"task-1","name":"Follow up"}""");

        var client = CreateClient(handler);

        var result = await client.CreateAsync(new CreateTaskRequest
        {
            Name = "Follow up",
            TaskTypeId = "call",
            Date = DateTimeOffset.Now
        });

        result.Id.Should().Be("task-1");
        result.Name.Should().Be("Follow up");
    }

    [Fact]
    public async Task ListTaskTypesAsync_ReturnsTaskTypes()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """[{"id":"call","title":"Call"}]""");

        var client = CreateClient(handler);

        var result = await client.ListTaskTypesAsync();

        result.Should().HaveCount(1);
        result[0].Id.Should().Be("call");
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
                Content = new StringContent("""{"count":0,"tasks":[]}""")
            });
        });

        var client = CreateClient(handler);

        await client.ListAsync(new ListTasksRequest
        {
            FilterTaskTypeId = "call",
            FilterDone = true
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.RequestUri!.Query.Should().Contain("filter[taskTypeId]=call");
        capturedRequest.RequestUri.Query.Should().Contain("filter[done]=true");
    }
}
