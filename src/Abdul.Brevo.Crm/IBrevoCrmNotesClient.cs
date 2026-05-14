using Abdul.Brevo.Crm.Models.Notes;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing notes in the Brevo Sales CRM.
/// </summary>
public interface IBrevoCrmNotesClient
{
    /// <summary>
    /// Gets a paginated list of notes based on filters.
    /// </summary>
    Task<CrmNoteListResponse> ListAsync(
        ListNotesRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new note.
    /// </summary>
    Task<CrmNote> CreateAsync(
        CreateNoteRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets full details of a specific note.
    /// </summary>
    Task<CrmNote> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing note.
    /// </summary>
    Task UpdateAsync(
        string id,
        UpdateNoteRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a note.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);
}
