using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Common;

/// <summary>
/// Request for linking or unlinking entities to a parent object.
/// </summary>
public sealed class LinkUnlinkRequest
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
    /// IDs of deals to link.
    /// </summary>
    [JsonPropertyName("linkDealIds")]
    public List<string>? LinkDealIds { get; set; }

    /// <summary>
    /// IDs of deals to unlink.
    /// </summary>
    [JsonPropertyName("unlinkDealIds")]
    public List<string>? UnlinkDealIds { get; set; }
}
