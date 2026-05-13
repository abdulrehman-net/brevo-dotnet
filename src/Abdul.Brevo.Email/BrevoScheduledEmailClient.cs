using Abdul.Brevo.Email.Models;

namespace Abdul.Brevo.Email;

internal sealed class BrevoScheduledEmailClient : IBrevoScheduledEmailClient
{
    private readonly BrevoEmailHttpClient _client;

    public BrevoScheduledEmailClient(BrevoEmailHttpClient client)
    {
        _client = client;
    }

    public Task<ScheduledEmailInfo> GetScheduledAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identifier);

        return _client.GetAsync<ScheduledEmailInfo>(
            $"/v3/smtp/emails/{Uri.EscapeDataString(identifier)}",
            cancellationToken);
    }

    public Task DeleteScheduledAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identifier);

        return _client.DeleteAsync(
            $"/v3/smtp/emails/{Uri.EscapeDataString(identifier)}",
            cancellationToken);
    }
}
