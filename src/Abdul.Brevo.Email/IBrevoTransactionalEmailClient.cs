using Abdul.Brevo.Email.Models;

namespace Abdul.Brevo.Email;

/// <summary>
/// Client for sending and retrieving transactional emails via the Brevo API.
/// </summary>
public interface IBrevoTransactionalEmailClient
{
    /// <summary>
    /// Sends a transactional email.
    /// </summary>
    Task<SendTransactionalEmailResponse> SendAsync(
        SendTransactionalEmailRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of transactional emails based on filters.
    /// </summary>
    Task<TransactionalEmailListResponse> ListAsync(
        ListTransactionalEmailsRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the personalized content of a sent transactional email.
    /// </summary>
    Task<TransactionalEmailContent> GetContentAsync(
        string messageId,
        CancellationToken cancellationToken = default);
}
