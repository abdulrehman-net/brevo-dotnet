using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Reply-to address for a transactional email.
/// </summary>
public sealed class EmailReplyTo
{
    /// <summary>
    /// Reply-to email address.
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    /// <summary>
    /// Display name for the reply-to address.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
