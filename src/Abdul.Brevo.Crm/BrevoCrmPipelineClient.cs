using Abdul.Brevo.Crm.Models.Pipelines;

namespace Abdul.Brevo.Crm;

internal sealed class BrevoCrmPipelineClient : IBrevoCrmPipelineClient
{
    private readonly BrevoCrmHttpClient _client;

    public BrevoCrmPipelineClient(BrevoCrmHttpClient client)
    {
        _client = client;
    }

    public Task<List<Pipeline>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetAsync<List<Pipeline>>(
            "/v3/crm/pipeline/details",
            cancellationToken);
    }

    public Task<Pipeline> GetAsync(
        string pipelineId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pipelineId);

        return _client.GetAsync<Pipeline>(
            $"/v3/crm/pipeline/details/{Uri.EscapeDataString(pipelineId)}",
            cancellationToken);
    }
}
