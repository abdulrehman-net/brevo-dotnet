namespace Abdul.Brevo.Email;

/// <summary>
/// Configuration options for the Brevo Transactional Email SDK.
/// </summary>
public sealed class BrevoEmailOptions
{
    /// <summary>
    /// The configuration section name used when binding from <c>IConfiguration</c>.
    /// </summary>
    public const string SectionName = "BrevoEmail";

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
