using System.Net;
using Abdul.Brevo.Crm.Models.Notes;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmNotesClientTests
{
    private static BrevoCrmNotesClient CreateClient(
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
        return new BrevoCrmNotesClient(brevoHttpClient);
    }

    [Fact]
    public async Task CreateAsync_ReturnsNote()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.Created,
            """{"id":"note-1","text":"Great call"}""");

        var client = CreateClient(handler);

        var result = await client.CreateAsync(new CreateNoteRequest
        {
            Text = "Great call"
        });

        result.Id.Should().Be("note-1");
        result.Text.Should().Be("Great call");
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
                Content = new StringContent("""{"count":0,"notes":[]}""")
            });
        });

        var client = CreateClient(handler);

        await client.ListAsync(new ListNotesRequest
        {
            Entity = "deals",
            EntityIds = "deal-1,deal-2"
        });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.RequestUri!.Query.Should().Contain("entity=deals");
        capturedRequest.RequestUri.Query.Should().Contain("entityIds=deal-1%2Cdeal-2");
    }
}
