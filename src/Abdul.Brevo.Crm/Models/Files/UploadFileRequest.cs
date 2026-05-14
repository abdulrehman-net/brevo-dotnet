namespace Abdul.Brevo.Crm.Models.Files;

/// <summary>
/// Request for uploading a file to the CRM.
/// </summary>
public sealed class UploadFileRequest
{
    /// <summary>
    /// The file content stream.
    /// </summary>
    public required Stream FileStream { get; set; }

    /// <summary>
    /// The name of the file (including extension).
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Optional contact ID to link the file to.
    /// </summary>
    public long? ContactId { get; set; }

    /// <summary>
    /// Optional company ID to link the file to.
    /// </summary>
    public string? CompanyId { get; set; }

    /// <summary>
    /// Optional deal ID to link the file to.
    /// </summary>
    public string? DealId { get; set; }
}
