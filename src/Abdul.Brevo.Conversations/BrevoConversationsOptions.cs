namespace Abdul.Brevo.Conversations;

public sealed class BrevoConversationsOptions
{
    public const string SectionName = "BrevoConversations";

    public string BaseUrl { get; set; } = "https://api.brevo.com";

    public string ApiKey { get; set; } = string.Empty;

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
