namespace Abdul.Brevo.Abstractions;

/// <summary>
/// Base configuration shared by all Brevo SDK modules.
/// </summary>
public abstract class BrevoOptionsBase
{
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
