using Abdul.Brevo.Crm.Models.Tasks;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing tasks and task types in the Brevo Sales CRM.
/// </summary>
public interface IBrevoCrmTasksClient
{
    /// <summary>
    /// Gets a paginated list of tasks based on filters.
    /// </summary>
    Task<CrmTaskListResponse> ListAsync(
        ListTasksRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new task.
    /// </summary>
    Task<CrmTask> CreateAsync(
        CreateTaskRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets full details of a specific task.
    /// </summary>
    Task<CrmTask> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    Task UpdateAsync(
        string id,
        UpdateTaskRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a task.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all available task types.
    /// </summary>
    Task<List<CrmTaskType>> ListTaskTypesAsync(
        CancellationToken cancellationToken = default);
}
