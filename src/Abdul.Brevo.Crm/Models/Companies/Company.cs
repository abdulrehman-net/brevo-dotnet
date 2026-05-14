using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Companies;

/// <summary>
/// Represents a company in the Brevo CRM.
/// </summary>
public sealed class Company
{
    /// <summary>
    /// Unique identifier for the company.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Dynamic attributes of the company.
    /// </summary>
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// IDs of contacts linked to this company.
    /// </summary>
    [JsonPropertyName("linkedContactsIds")]
    public List<long>? LinkedContactsIds { get; set; }

    /// <summary>
    /// IDs of deals linked to this company.
    /// </summary>
    [JsonPropertyName("linkedDealsIds")]
    public List<string>? LinkedDealsIds { get; set; }
}
