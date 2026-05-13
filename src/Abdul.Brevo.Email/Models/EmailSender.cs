using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Represents the sender of a transactional email.
/// Specify either <see cref="Id"/> (a pre-configured sender in Brevo) or <see cref="Email"/> with an optional <see cref="Name"/>.
/// </summary>
public sealed class EmailSender
{
    /// <summary>
    /// Email address of the sender.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Display name of the sender. Ignored when <see cref="Id"/> is provided.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// ID of a pre-configured sender in Brevo.
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id { get; set; }
}
