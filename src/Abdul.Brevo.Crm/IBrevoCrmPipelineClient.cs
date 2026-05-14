using Abdul.Brevo.Crm.Models.Pipelines;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Client for managing CRM pipelines and stages.
/// </summary>
public interface IBrevoCrmPipelineClient
{
    /// <summary>
    /// Gets all CRM pipelines and their stages.
    /// </summary>
    Task<List<Pipeline>> ListAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets details of a specific CRM pipeline.
    /// </summary>
    Task<Pipeline> GetAsync(
        string pipelineId,
        CancellationToken cancellationToken = default);
}
