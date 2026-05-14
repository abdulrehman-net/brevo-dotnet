namespace Abdul.Brevo.Crm;

/// <summary>
/// Configuration options for the Brevo Sales CRM SDK.
/// </summary>
public sealed class BrevoCrmOptions
{
    /// <summary>
    /// The configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "Brevo";

    /// <summary>
    /// Base URL for the Brevo API. Defaults to <c>https://api.brevo.com</c>.
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.brevo.com";

    /// <summary>
    /// Your Brevo API key.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// HTTP request timeout. Defaults to 30 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
