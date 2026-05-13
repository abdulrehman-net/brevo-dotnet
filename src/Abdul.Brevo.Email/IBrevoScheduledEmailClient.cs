using Abdul.Brevo.Email.Models;

namespace Abdul.Brevo.Email;

/// <summary>
/// Client for managing scheduled transactional emails.
/// </summary>
public interface IBrevoScheduledEmailClient
{
    /// <summary>
    /// Fetches scheduled emails by batchId or messageId.
    /// </summary>
    Task<ScheduledEmailInfo> GetScheduledAsync(
        string identifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes scheduled emails by batchId or messageId.
    /// </summary>
    Task DeleteScheduledAsync(
        string identifier,
        CancellationToken cancellationToken = default);
}
