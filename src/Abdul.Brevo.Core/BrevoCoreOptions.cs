using Abdul.Brevo.Abstractions;

namespace Abdul.Brevo.Core;

/// <summary>
/// Configuration options for the Brevo Core SDK.
/// </summary>
public sealed class BrevoCoreOptions : BrevoOptionsBase
{
    /// <summary>
    /// The configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "Brevo";
}
