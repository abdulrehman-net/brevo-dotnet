using System.Text.Json.Serialization;
using Abdul.Brevo.Abstractions.Pagination;
using Abdul.Brevo.Core.Models;

namespace Abdul.Brevo.Core;

/// <summary>
/// Client for interacting with the Brevo Lists API.
/// </summary>
public interface IBrevoListsClient
{
    /// <summary>
    /// Retrieves a paginated list of contact lists.
    /// </summary>
    Task<BrevoPagedResponse<BrevoList>> ListAsync(
        GetListsRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new contact list.
    /// </summary>
    Task<BrevoList> CreateAsync(
        CreateListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list by its ID.
    /// </summary>
    Task<BrevoList> GetAsync(
        long listId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a list by its ID.
    /// </summary>
    Task UpdateAsync(
        long listId,
        UpdateListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a list by its ID.
    /// </summary>
    Task DeleteAsync(
        long listId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds existing contacts to a list.
    /// </summary>
    Task AddContactsAsync(
        long listId,
        ModifyListContactsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes contacts from a list.
    /// </summary>
    Task RemoveContactsAsync(
        long listId,
        ModifyListContactsRequest request,
        CancellationToken cancellationToken = default);
}

internal sealed class BrevoListsClient : IBrevoListsClient
{
    private readonly BrevoCoreHttpClient _httpClient;

    public BrevoListsClient(BrevoCoreHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<BrevoPagedResponse<BrevoList>> ListAsync(
        GetListsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var path = "v3/contacts/lists";
        path = request != null ? request.AppendTo(path) : new BrevoPagedRequest().AppendTo(path);

        return _httpClient.GetAsync<BrevoPagedResponse<BrevoList>>(path, cancellationToken);
    }

    public async Task<BrevoList> CreateAsync(
        CreateListRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.PostAsync<CreateListRequest, CreateListResponse>("v3/contacts/lists", request, cancellationToken);
        return await GetAsync(result.Id, cancellationToken);
    }

    public Task<BrevoList> GetAsync(
        long listId,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.GetAsync<BrevoList>($"v3/contacts/lists/{listId}", cancellationToken);
    }

    public Task UpdateAsync(
        long listId,
        UpdateListRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PutAsync<UpdateListRequest, object>($"v3/contacts/lists/{listId}", request, cancellationToken);
    }

    public Task DeleteAsync(
        long listId,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.DeleteAsync($"v3/contacts/lists/{listId}", cancellationToken);
    }

    public Task AddContactsAsync(
        long listId,
        ModifyListContactsRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PostAsync<ModifyListContactsRequest, object>($"v3/contacts/lists/{listId}/contacts/add", request, cancellationToken);
    }

    public Task RemoveContactsAsync(
        long listId,
        ModifyListContactsRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PostAsync<ModifyListContactsRequest, object>($"v3/contacts/lists/{listId}/contacts/remove", request, cancellationToken);
    }
}

internal sealed class CreateListResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
}
