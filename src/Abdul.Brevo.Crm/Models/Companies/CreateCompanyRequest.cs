using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Companies;

/// <summary>
/// Request for creating a new company.
/// </summary>
public sealed class CreateCompanyRequest
{
    /// <summary>
    /// Name of the company.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Initial attributes for the company.
    /// </summary>
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }
}
