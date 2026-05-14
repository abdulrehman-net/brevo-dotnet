using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Companies;

/// <summary>
/// Response from listing companies.
/// </summary>
public sealed class CompanyListResponse
{
    /// <summary>
    /// Total number of companies matching the filter.
    /// </summary>
    [JsonPropertyName("count")]
    public long? Count { get; set; }

    /// <summary>
    /// List of companies.
    /// </summary>
    [JsonPropertyName("companies")]
    public List<Company>? Companies { get; set; }
}
