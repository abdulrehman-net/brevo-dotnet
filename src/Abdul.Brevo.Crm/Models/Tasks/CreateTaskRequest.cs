using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Tasks;

/// <summary>
/// Request for creating a new task.
/// </summary>
public sealed class CreateTaskRequest
{
    /// <summary>
    /// Name of the task.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// ID of the task type.
    /// </summary>
    [JsonPropertyName("taskTypeId")]
    public required string TaskTypeId { get; set; }

    /// <summary>
    /// Due date and time for the task.
    /// </summary>
    [JsonPropertyName("date")]
    public required DateTimeOffset Date { get; set; }

    /// <summary>
    /// Duration of the task in milliseconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    /// <summary>
    /// Additional notes for the task.
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Whether the task is completed.
    /// </summary>
    [JsonPropertyName("done")]
    public bool? Done { get; set; }

    /// <summary>
    /// ID of the user assigned to this task.
    /// </summary>
    [JsonPropertyName("assignToId")]
    public string? AssignToId { get; set; }

    /// <summary>
    /// IDs of contacts linked to this task.
    /// </summary>
    [JsonPropertyName("contactsIds")]
    public List<long>? ContactsIds { get; set; }

    /// <summary>
    /// IDs of deals linked to this task.
    /// </summary>
    [JsonPropertyName("dealsIds")]
    public List<string>? DealsIds { get; set; }

    /// <summary>
    /// IDs of companies linked to this task.
    /// </summary>
    [JsonPropertyName("companiesIds")]
    public List<string>? CompaniesIds { get; set; }
}
