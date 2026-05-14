using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Deals;

/// <summary>
/// Response from listing deals.
/// </summary>
public sealed class DealListResponse
{
    /// <summary>
    /// Total number of deals matching the filter.
    /// </summary>
    [JsonPropertyName("count")]
    public long? Count { get; set; }

    /// <summary>
    /// List of deals.
    /// </summary>
    [JsonPropertyName("deals")]
    public List<Deal>? Deals { get; set; }
}
