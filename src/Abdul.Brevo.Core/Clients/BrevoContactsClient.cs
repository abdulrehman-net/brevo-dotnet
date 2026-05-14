using Abdul.Brevo.Abstractions.Pagination;
using Abdul.Brevo.Core.Models;

namespace Abdul.Brevo.Core;

/// <summary>
/// Client for interacting with the Brevo Contacts API.
/// </summary>
public interface IBrevoContactsClient
{
    /// <summary>
    /// Retrieves a paginated list of contacts.
    /// </summary>
    Task<BrevoPagedResponse<BrevoContact>> ListAsync(
        GetContactsRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new contact.
    /// </summary>
    Task CreateAsync(
        CreateContactRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a contact by their email address or ID.
    /// </summary>
    Task<BrevoContact> GetAsync(
        string identifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a contact by their email address or ID.
    /// </summary>
    Task UpdateAsync(
        string identifier,
        UpdateContactRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a contact by their email address or ID.
    /// </summary>
    Task DeleteAsync(
        string identifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a contact using the Double Opt-In (DOI) flow.
    /// </summary>
    Task CreateViaDoubleOptInAsync(
        CreateDoubleOptInContactRequest request,
        CancellationToken cancellationToken = default);
}

internal sealed class BrevoContactsClient : IBrevoContactsClient
{
    private readonly BrevoCoreHttpClient _httpClient;

    public BrevoContactsClient(BrevoCoreHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<BrevoPagedResponse<BrevoContact>> ListAsync(
        GetContactsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var path = "v3/contacts";
        if (request != null)
        {
            path = request.AppendTo(path);
        }
        else
        {
            path = new BrevoPagedRequest().AppendTo(path);
        }

        return _httpClient.GetAsync<BrevoPagedResponse<BrevoContact>>(path, cancellationToken);
    }

    public Task CreateAsync(
        CreateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PostNoContentAsync("v3/contacts", request, cancellationToken);
    }

    public Task<BrevoContact> GetAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var encodedIdentifier = Uri.EscapeDataString(identifier);
        return _httpClient.GetAsync<BrevoContact>($"v3/contacts/{encodedIdentifier}", cancellationToken);
    }

    public Task UpdateAsync(
        string identifier,
        UpdateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        var encodedIdentifier = Uri.EscapeDataString(identifier);
        return _httpClient.PutAsync<UpdateContactRequest, object>($"v3/contacts/{encodedIdentifier}", request, cancellationToken);
    }

    public Task DeleteAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var encodedIdentifier = Uri.EscapeDataString(identifier);
        return _httpClient.DeleteAsync($"v3/contacts/{encodedIdentifier}", cancellationToken);
    }

    public Task CreateViaDoubleOptInAsync(
        CreateDoubleOptInContactRequest request,
        CancellationToken cancellationToken = default)
    {
        return _httpClient.PostNoContentAsync("v3/contacts/doubleOptin", request, cancellationToken);
    }
}
