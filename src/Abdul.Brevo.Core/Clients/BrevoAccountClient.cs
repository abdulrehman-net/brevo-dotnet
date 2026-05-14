using Abdul.Brevo.Core.Models;

namespace Abdul.Brevo.Core;

/// <summary>
/// Client for interacting with the Brevo Account API.
/// </summary>
public interface IBrevoAccountClient
{
    /// <summary>
    /// Retrieves the details of your Brevo account, including your plan limits and configuration.
    /// </summary>
    Task<BrevoAccount> GetAsync(CancellationToken cancellationToken = default);
}

internal sealed class BrevoAccountClient : IBrevoAccountClient
{
    private readonly BrevoCoreHttpClient _httpClient;

    public BrevoAccountClient(BrevoCoreHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<BrevoAccount> GetAsync(CancellationToken cancellationToken = default)
    {
        return _httpClient.GetAsync<BrevoAccount>("v3/account", cancellationToken);
    }
}
