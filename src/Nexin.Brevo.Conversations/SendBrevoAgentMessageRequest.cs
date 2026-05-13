using System.Text.Json.Serialization;

namespace Nexin.Brevo.Conversations;

public sealed class SendBrevoAgentMessageRequest
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("visitorId")]
    public required string VisitorId { get; init; }

    [JsonPropertyName("agentId")]
    public string? AgentId { get; init; }

    [JsonPropertyName("agentEmail")]
    public string? AgentEmail { get; init; }

    [JsonPropertyName("agentName")]
    public string? AgentName { get; init; }

    [JsonPropertyName("receivedFrom")]
    public string? ReceivedFrom { get; init; }
}
