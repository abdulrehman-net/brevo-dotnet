using Abdul.Brevo.Abstractions;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Configuration options for the Brevo Sales CRM SDK.
/// </summary>
public sealed class BrevoCrmOptions : BrevoOptionsBase
{
    /// <summary>
    /// The configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "Brevo";
}
