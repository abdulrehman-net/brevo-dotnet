using System.Text.Json.Serialization;

namespace Nexin.Brevo.Conversations;

public sealed class BrevoConversationMessage
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("visitorId")]
    public string? VisitorId { get; init; }

    [JsonPropertyName("agentId")]
    public string? AgentId { get; init; }

    [JsonPropertyName("agentName")]
    public string? AgentName { get; init; }

    [JsonPropertyName("agentUserpic")]
    public string? AgentUserpic { get; init; }

    [JsonPropertyName("text")]
    public string? Text { get; init; }

    [JsonPropertyName("html")]
    public string? Html { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("createdAt")]
    public long? CreatedAt { get; init; }

    [JsonPropertyName("isPushed")]
    public bool? IsPushed { get; init; }

    [JsonPropertyName("isTrigger")]
    public bool? IsTrigger { get; init; }

    [JsonPropertyName("isMissed")]
    public bool? IsMissed { get; init; }

    [JsonPropertyName("isMissedByVisitor")]
    public bool? IsMissedByVisitor { get; init; }

    [JsonPropertyName("receivedFrom")]
    public string? ReceivedFrom { get; init; }

    [JsonPropertyName("rawUnsafeHtml")]
    public string? RawUnsafeHtml { get; init; }

    [JsonPropertyName("sourceMessageId")]
    public string? SourceMessageId { get; init; }

    [JsonPropertyName("subject")]
    public string? Subject { get; init; }

    [JsonPropertyName("messageType")]
    public string? MessageType { get; init; }

    [JsonPropertyName("isForward")]
    public bool? IsForward { get; init; }

    [JsonPropertyName("isSentViaJsApi")]
    public bool? IsSentViaJsApi { get; init; }
}
