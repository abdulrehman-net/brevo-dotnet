using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Deals;

/// <summary>
/// Request for linking or unlinking contacts and companies to a specific deal.
/// </summary>
public sealed class DealLinkUnlinkRequest
{
    /// <summary>
    /// IDs of contacts to link.
    /// </summary>
    [JsonPropertyName("linkContactIds")]
    public List<long>? LinkContactIds { get; set; }

    /// <summary>
    /// IDs of contacts to unlink.
    /// </summary>
    [JsonPropertyName("unlinkContactIds")]
    public List<long>? UnlinkContactIds { get; set; }

    /// <summary>
    /// IDs of companies to link.
    /// </summary>
    [JsonPropertyName("linkCompanyIds")]
    public List<string>? LinkCompanyIds { get; set; }

    /// <summary>
    /// IDs of companies to unlink.
    /// </summary>
    [JsonPropertyName("unlinkCompanyIds")]
    public List<string>? UnlinkCompanyIds { get; set; }
}
