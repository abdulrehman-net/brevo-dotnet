using System.Text.Json.Serialization;

namespace Nexin.Brevo.Conversations;

public sealed class UpdateBrevoMessageRequest
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
