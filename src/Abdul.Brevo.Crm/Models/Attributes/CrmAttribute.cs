using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Attributes;

/// <summary>
/// Represents a CRM attribute definition.
/// </summary>
public sealed class CrmAttribute
{
    /// <summary>
    /// The internal name of the attribute.
    /// </summary>
    [JsonPropertyName("internalName")]
    public string? InternalName { get; set; }

    /// <summary>
    /// The display label of the attribute.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// The type of the attribute (text, number, date, single-select, etc.).
    /// </summary>
    [JsonPropertyName("attributeTypeName")]
    public string? AttributeTypeName { get; set; }

    /// <summary>
    /// Whether the attribute is required.
    /// </summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; set; }

    /// <summary>
    /// The available options for single-select or multi-choice attributes.
    /// </summary>
    [JsonPropertyName("optionsLabels")]
    public List<string>? OptionsLabels { get; set; }
}
