using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Attributes;

/// <summary>
/// Request for creating a new custom CRM attribute.
/// </summary>
public sealed class CreateCrmAttributeRequest
{
    /// <summary>
    /// The display label for the attribute.
    /// </summary>
    [JsonPropertyName("label")]
    public required string Label { get; set; }

    /// <summary>
    /// The type of the attribute (e.g., text, number, single-select).
    /// </summary>
    [JsonPropertyName("attributeType")]
    public required string AttributeType { get; set; }

    /// <summary>
    /// The object type the attribute belongs to (companies or deals).
    /// </summary>
    [JsonPropertyName("objectType")]
    public required string ObjectType { get; set; }

    /// <summary>
    /// The available options for single-select or multi-choice attributes.
    /// </summary>
    [JsonPropertyName("optionsLabels")]
    public List<string>? OptionsLabels { get; set; }
}
