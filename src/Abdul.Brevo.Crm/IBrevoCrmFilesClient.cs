using Abdul.Brevo.Crm.Models.Files;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing files in the Brevo Sales CRM.
/// </summary>
public interface IBrevoCrmFilesClient
{
    /// <summary>
    /// Gets a paginated list of CRM files based on filters.
    /// </summary>
    Task<CrmFileListResponse> ListAsync(
        ListFilesRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a file to the CRM and optionally links it to an entity.
    /// </summary>
    Task<CrmFile> UploadAsync(
        UploadFileRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a temporary download URL for a CRM file.
    /// </summary>
    Task<FileDownloadResponse> GetDownloadUrlAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a file from the CRM.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);
}
