using Abdul.Brevo.Email.Models;

namespace Abdul.Brevo.Email;

/// <summary>
/// Client for managing hard bounces.
/// </summary>
public interface IBrevoHardBounceClient
{
    /// <summary>
    /// Deletes hard bounces. Can filter by date range or specific contact email.
    /// </summary>
    Task DeleteAsync(
        DeleteHardBouncesRequest request,
        CancellationToken cancellationToken = default);
}
