using Abdul.Brevo.Abstractions;

namespace Abdul.Brevo.Email;

/// <summary>
/// Configuration options for the Brevo Transactional Email SDK.
/// </summary>
public sealed class BrevoEmailOptions : BrevoOptionsBase
{
    /// <summary>
    /// The configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "Brevo";
}
