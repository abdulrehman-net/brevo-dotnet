using System.Text.Json.Serialization;

namespace Abdul.Brevo.Abstractions.Http;

/// <summary>
/// Represents the standard JSON error body returned by the Brevo API
/// when a request fails: <c>{ "code": "...", "message": "..." }</c>.
/// </summary>
public sealed class BrevoErrorResponse
{
    /// <summary>
    /// Machine-readable error code (e.g. <c>invalid_parameter</c>, <c>unauthorized</c>,
    /// <c>not_enough_credits</c>).
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>
    /// Human-readable description of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
