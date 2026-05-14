using System.Text.Json.Serialization;
using Abdul.Brevo.Abstractions.Pagination;
using Abdul.Brevo.Core.Models;

namespace Abdul.Brevo.Core;

/// <summary>
/// Client for interacting with the Brevo Folders API.
/// </summary>
public interface IBrevoFoldersClient
{
    /// <summary>
    /// Retrieves a paginated list of folders.
    /// </summary>
    Task<BrevoPagedResponse<BrevoFolder>> ListAsync(
        GetFoldersRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new folder.
    /// </summary>
    Task<BrevoFolder> CreateAsync(
        CreateFolderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a folder by its ID.
    /// </summary>
    Task<BrevoFolder> GetAsync(
        long folderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a folder by its ID.
    /// </summary>
    Task UpdateAsync(
        long folderId,
        UpdateFolderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a folder by its ID.
    /// </summary>
    Task DeleteAsync(
        long folderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of contact lists within a specific folder.
    /// </summary>
    Task<BrevoPagedResponse<BrevoList>> GetListsAsync(
        long folderId,
        GetListsRequest? request = null,
        CancellationToken cancellationToken = default);
}

internal sealed class BrevoFoldersClient : IBrevoFoldersClient
{
    private readonly BrevoCoreHttpClient _httpClient;

    public BrevoFoldersClient(BrevoCoreHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<BrevoPagedResponse<BrevoFolder>> ListAsync(
        GetFoldersRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var path = "v3/contacts/folders";
        path = request != null ? request.AppendTo(path) : new BrevoPagedRequest().AppendTo(path);

        return _httpClient.GetAsync<BrevoPagedResponse<BrevoFolder>>(path, cancellationToken);
    }

    public async Task<BrevoFolder> CreateAsync(
        CreateFolderRequest request,
        CancellationToken cancellationToken = default)
    {
        // The create endpoint returns { "id": 123 }, so we need a tiny DTO or just fetch it again
        var result = await _httpClient.PostAsync<CreateFolderRequest, CreateFolderResponse>("v3/contacts/folders", request, cancellationToken);
        return await GetAsync(result.Id, cancellationToken);
    }

    public Task<BrevoFolder> GetAsync(
        long folderId,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.GetAsync<BrevoFolder>($"v3/contacts/folders/{folderId}", cancellationToken);
    }

    public Task UpdateAsync(
        long folderId,
        UpdateFolderRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PutAsync<UpdateFolderRequest, object>($"v3/contacts/folders/{folderId}", request, cancellationToken);
    }

    public Task DeleteAsync(
        long folderId,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.DeleteAsync($"v3/contacts/folders/{folderId}", cancellationToken);
    }

    public Task<BrevoPagedResponse<BrevoList>> GetListsAsync(
        long folderId,
        GetListsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var path = $"v3/contacts/folders/{folderId}/lists";
        path = request != null ? request.AppendTo(path) : new BrevoPagedRequest().AppendTo(path);

        return _httpClient.GetAsync<BrevoPagedResponse<BrevoList>>(path, cancellationToken);
    }
}

internal sealed class CreateFolderResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
}
