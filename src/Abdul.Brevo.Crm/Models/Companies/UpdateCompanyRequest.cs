using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Companies;

/// <summary>
/// Request for updating an existing company.
/// </summary>
public sealed class UpdateCompanyRequest
{
    /// <summary>
    /// Updated name of the company.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Updated attributes for the company.
    /// </summary>
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }
}
