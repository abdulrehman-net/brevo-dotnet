using System.Text.Json.Serialization;

namespace Abdul.Brevo.Conversations;

/// <summary>
/// Represents the JSON error body returned by the Brevo API
/// when a request fails.
/// </summary>
internal sealed class BrevoErrorResponse
{
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
