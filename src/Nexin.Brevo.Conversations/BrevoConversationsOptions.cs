namespace Nexin.Brevo.Conversations;

public sealed class BrevoConversationsOptions
{
    public const string SectionName = "BrevoConversations";

    public string BaseUrl { get; init; } = "https://api.brevo.com";

    public string ApiKey { get; init; } = string.Empty;

    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(30);
}
