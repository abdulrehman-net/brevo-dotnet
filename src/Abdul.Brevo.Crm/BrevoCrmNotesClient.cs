using Abdul.Brevo.Crm.Models.Notes;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmNotesClient : IBrevoCrmNotesClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmNotesClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task<CrmNoteListResponse> ListAsync(
        ListNotesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = request?.ToQueryString() ?? string.Empty;

        return _client.GetAsync<CrmNoteListResponse>(
            $"/v3/crm/notes{queryString}",
            cancellationToken);
    }

    public Task<CrmNote> CreateAsync(
        CreateNoteRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostAsync<CreateNoteRequest, CrmNote>(
            "/v3/crm/notes",
            request,
            cancellationToken);
    }

    public Task<CrmNote> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.GetAsync<CrmNote>(
            $"/v3/crm/notes/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task UpdateAsync(
        string id,
        UpdateNoteRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(request);

        return _client.PatchNoContentAsync(
            $"/v3/crm/notes/{Uri.EscapeDataString(id)}",
            request,
            cancellationToken);
    }

    public Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.DeleteAsync(
            $"/v3/crm/notes/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }
}
