using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Deals;

/// <summary>
/// Request for updating an existing deal.
/// </summary>
public sealed class UpdateDealRequest
{
    /// <summary>
    /// Updated name of the deal.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Updated attributes for the deal.
    /// </summary>
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }
}
