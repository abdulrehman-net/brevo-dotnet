using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Pipelines;

/// <summary>
/// Represents a CRM pipeline.
/// </summary>
public sealed class Pipeline
{
    /// <summary>
    /// Unique identifier for the pipeline.
    /// </summary>
    [JsonPropertyName("pipelineId")]
    public string? PipelineId { get; set; }

    /// <summary>
    /// Name of the pipeline.
    /// </summary>
    [JsonPropertyName("pipelineName")]
    public string? PipelineName { get; set; }

    /// <summary>
    /// Stages configured for this pipeline.
    /// </summary>
    [JsonPropertyName("stages")]
    public List<PipelineStage>? Stages { get; set; }
}
