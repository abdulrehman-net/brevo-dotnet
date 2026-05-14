using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Tasks;

/// <summary>
/// Represents a type of task in the CRM.
/// </summary>
public sealed class CrmTaskType
{
    /// <summary>
    /// Unique identifier for the task type.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Title/Name of the task type.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}
