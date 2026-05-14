using System.Text.Json.Serialization;

namespace Abdul.Brevo.Crm.Models.Tasks;

/// <summary>
/// Request for updating an existing task.
/// </summary>
public sealed class UpdateTaskRequest
{
    /// <summary>
    /// Updated name of the task.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Updated ID of the task type.
    /// </summary>
    [JsonPropertyName("taskTypeId")]
    public string? TaskTypeId { get; set; }

    /// <summary>
    /// Updated due date and time for the task.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTimeOffset? Date { get; set; }

    /// <summary>
    /// Updated duration of the task in milliseconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    /// <summary>
    /// Updated notes for the task.
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Updated status of the task.
    /// </summary>
    [JsonPropertyName("done")]
    public bool? Done { get; set; }

    /// <summary>
    /// Updated ID of the user assigned to this task.
    /// </summary>
    [JsonPropertyName("assignToId")]
    public string? AssignToId { get; set; }

    /// <summary>
    /// Updated IDs of contacts linked to this task.
    /// </summary>
    [JsonPropertyName("contactsIds")]
    public List<long>? ContactsIds { get; set; }

    /// <summary>
    /// Updated IDs of deals linked to this task.
    /// </summary>
    [JsonPropertyName("dealsIds")]
    public List<string>? DealsIds { get; set; }

    /// <summary>
    /// Updated IDs of companies linked to this task.
    /// </summary>
    [JsonPropertyName("companiesIds")]
    public List<string>? CompaniesIds { get; set; }
}
