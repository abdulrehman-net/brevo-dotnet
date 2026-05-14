using Abdul.Brevo.Crm.Models.Tasks;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmTasksClient : IBrevoCrmTasksClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmTasksClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task<CrmTaskListResponse> ListAsync(
        ListTasksRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = request?.ToQueryString() ?? string.Empty;

        return _client.GetAsync<CrmTaskListResponse>(
            $"/v3/crm/tasks{queryString}",
            cancellationToken);
    }

    public Task<CrmTask> CreateAsync(
        CreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostAsync<CreateTaskRequest, CrmTask>(
            "/v3/crm/tasks",
            request,
            cancellationToken);
    }

    public Task<CrmTask> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.GetAsync<CrmTask>(
            $"/v3/crm/tasks/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task UpdateAsync(
        string id,
        UpdateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(request);

        return _client.PatchNoContentAsync(
            $"/v3/crm/tasks/{Uri.EscapeDataString(id)}",
            request,
            cancellationToken);
    }

    public Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return _client.DeleteAsync(
            $"/v3/crm/tasks/{Uri.EscapeDataString(id)}",
            cancellationToken);
    }

    public Task<List<CrmTaskType>> ListTaskTypesAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetAsync<List<CrmTaskType>>(
            "/v3/crm/tasktypes",
            cancellationToken);
    }
}
