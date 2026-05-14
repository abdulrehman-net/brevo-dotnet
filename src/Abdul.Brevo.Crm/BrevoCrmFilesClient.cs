using System.Net.Http.Headers;
using Abdul.Brevo.Crm.Models.Files;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmFilesClient : IBrevoCrmFilesClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmFilesClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task<CrmFileListResponse> ListAsync(
        ListFilesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = request?.ToQueryString() ?? string.Empty;

        return _client.GetAsync<CrmFileListResponse>(
            $"/v3/crm/files{queryString}",
            cancellationToken);
    }

    public async Task<CrmFile> UploadAsync(
        UploadFileRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.FileStream);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.FileName);

        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(request.FileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(fileContent, "file", request.FileName);

        if (request.ContactId.HasValue)
            content.Add(new StringContent(request.ContactId.Value.ToString()), "contactId");

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            content.Add(new StringContent(request.CompanyId), "companyId");

        if (!string.IsNullOrWhiteSpace(request.DealId))
            content.Add(new StringContent(request.DealId), "dealId");

        return await _client.PostMultipartAsync<CrmFile>(
            "/v3/crm/files",
            content,
            cancellationToken);
    }

    public Task<FileDownloadResponse> GetDownloadUrlAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.GetAsync<FileDownloadResponse>(
            $"/v3/crm/files/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.DeleteAsync(
            $"/v3/crm/files/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }
}
