using Abdul.Brevo.Email.Models;

namespace Abdul.Brevo.Email;

internal sealed class BrevoHardBounceClient : IBrevoHardBounceClient
{
    private readonly BrevoEmailHttpClient _client;

    public BrevoHardBounceClient(BrevoEmailHttpClient client)
    {
        _client = client;
    }

    public Task DeleteAsync(
        DeleteHardBouncesRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostNoContentAsync(
            "/v3/smtp/deleteHardbounces",
            request,
            cancellationToken);
    }
}
