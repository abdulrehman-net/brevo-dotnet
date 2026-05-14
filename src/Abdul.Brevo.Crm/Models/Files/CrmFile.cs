using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Files;

/// <summary>
/// Represents a file in the CRM.
/// </summary>
public sealed class CrmFile
{
    /// <summary>
    /// Unique identifier for the file.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Name of the file.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Size of the file in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    /// Date and time when the file was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// ID of the contact linked to this file.
    /// </summary>
    [JsonPropertyName("contactId")]
    public long? ContactId { get; set; }

    /// <summary>
    /// ID of the deal linked to this file.
    /// </summary>
    [JsonPropertyName("dealId")]
    public string? DealId { get; set; }

    /// <summary>
    /// ID of the company linked to this file.
    /// </summary>
    [JsonPropertyName("companyId")]
    public string? CompanyId { get; set; }
}
