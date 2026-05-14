using Abdul.Brevo.Abstractions;

namespace Abdul.Brevo.Conversations;

/// <summary>
/// Configuration options for the Brevo Conversations SDK.
/// </summary>
public sealed class BrevoConversationsOptions : BrevoOptionsBase
{
    /// <summary>
    /// The configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "Brevo";
}
