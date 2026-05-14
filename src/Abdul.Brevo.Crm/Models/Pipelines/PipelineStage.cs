using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Pipelines;

/// <summary>
/// Represents a stage in a CRM pipeline.
/// </summary>
public sealed class PipelineStage
{
    /// <summary>
    /// Unique identifier for the stage.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Name of the stage.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
