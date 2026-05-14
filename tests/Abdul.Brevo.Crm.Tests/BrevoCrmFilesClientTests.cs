using System.Net;
using System.Text;
using Abdul.Brevo.Crm.Models.Files;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmFilesClientTests
{
    private static BrevoCrmFilesClient CreateClient(
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
        return new BrevoCrmFilesClient(brevoHttpClient);
    }

    [Fact]
    public async Task UploadAsync_SendsMultipartRequest()
    {
        HttpRequestMessage? capturedRequest = null;

        var handler = new HttpMessageHandlerMock((req, _) =>
        {
            capturedRequest = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent("""{"id":"file-1","name":"test.txt"}""")
            });
        });

        var client = CreateClient(handler);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
        var result = await client.UploadAsync(new UploadFileRequest
        {
            FileStream = stream,
            FileName = "test.txt",
            DealId = "deal-1"
        });

        result.Id.Should().Be("file-1");
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Content.Should().BeAssignableTo<MultipartFormDataContent>();
        
        var contentString = await capturedRequest.Content!.ReadAsStringAsync();
        contentString.Should().Contain("filename=test.txt");
        contentString.Should().Contain("name=\"dealId\"");
        contentString.Should().Contain("deal-1");
    }

    [Fact]
    public async Task GetDownloadUrlAsync_ReturnsUrl()
    {
        var handler = new HttpMessageHandlerMock(
            HttpStatusCode.OK,
            """{"fileUrl":"https://brevo.com/temp/123"}""");

        var client = CreateClient(handler);

        var result = await client.GetDownloadUrlAsync("file-1");

        result.FileUrl.Should().Be("https://brevo.com/temp/123");
    }
}
