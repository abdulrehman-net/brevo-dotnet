using System.Text.Json.Serialization;

namespace Nexin.Brevo.Conversations;

public sealed class SendBrevoAutomatedMessageRequest
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("visitorId")]
    public required string VisitorId { get; init; }

    [JsonPropertyName("agentId")]
    public string? AgentId { get; init; }

    [JsonPropertyName("groupId")]
    public string? GroupId { get; init; }
}
