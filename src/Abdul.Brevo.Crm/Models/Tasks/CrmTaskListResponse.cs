using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Tasks;

/// <summary>
/// Response from listing tasks.
/// </summary>
public sealed class CrmTaskListResponse
{
    /// <summary>
    /// Total number of tasks matching the filter.
    /// </summary>
    [JsonPropertyName("count")]
    public long? Count { get; set; }

    /// <summary>
    /// List of tasks.
    /// </summary>
    [JsonPropertyName("tasks")]
    public List<CrmTask>? Tasks { get; set; }
}
