using Abdul.Brevo.Email.Models;

namespace Abdul.Brevo.Email;

internal sealed class BrevoTransactionalEmailClient : IBrevoTransactionalEmailClient
{
    private readonly BrevoEmailHttpClient _client;

    public BrevoTransactionalEmailClient(BrevoEmailHttpClient client)
    {
        _client = client;
    }

    public Task<SendTransactionalEmailResponse> SendAsync(
        SendTransactionalEmailRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _client.PostAsync<SendTransactionalEmailRequest, SendTransactionalEmailResponse>(
            "/v3/smtp/email",
            request,
            cancellationToken);
    }

    public Task<TransactionalEmailListResponse> ListAsync(
        ListTransactionalEmailsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = request?.ToQueryString() ?? string.Empty;

        return _client.GetAsync<TransactionalEmailListResponse>(
            $"/v3/smtp/emails{queryString}",
            cancellationToken);
    }

    public Task<TransactionalEmailContent> GetContentAsync(
        string messageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.GetAsync<TransactionalEmailContent>(
            $"/v3/smtp/emails/{Uri.EscapeDataString(messageId)}",
            cancellationToken);
    }
}
