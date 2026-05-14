using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Deals;

/// <summary>
/// Represents a deal in the Brevo CRM.
/// </summary>
public sealed class Deal
{
    /// <summary>
    /// Unique identifier for the deal.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Dynamic attributes of the deal.
    /// </summary>
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// IDs of contacts linked to this deal.
    /// </summary>
    [JsonPropertyName("linkedContactsIds")]
    public List<long>? LinkedContactsIds { get; set; }

    /// <summary>
    /// IDs of companies linked to this deal.
    /// </summary>
    [JsonPropertyName("linkedCompaniesIds")]
    public List<string>? LinkedCompaniesIds { get; set; }
}
