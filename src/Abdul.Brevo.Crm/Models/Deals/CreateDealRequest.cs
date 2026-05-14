using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Deals;

/// <summary>
/// Request for creating a new deal.
/// </summary>
public sealed class CreateDealRequest
{
    /// <summary>
    /// Name of the deal.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Initial attributes for the deal.
    /// </summary>
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// Initial contacts linked to the deal.
    /// </summary>
    [JsonPropertyName("linkedContactsIds")]
    public List<long>? LinkedContactsIds { get; set; }

    /// <summary>
    /// Initial companies linked to the deal.
    /// </summary>
    [JsonPropertyName("linkedCompaniesIds")]
    public List<string>? LinkedCompaniesIds { get; set; }
}
