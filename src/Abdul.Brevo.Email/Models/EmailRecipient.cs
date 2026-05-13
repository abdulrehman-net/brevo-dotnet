using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Represents an email recipient with an address and optional display name.
/// </summary>
public sealed class EmailRecipient
{
    /// <summary>
    /// Email address of the recipient.
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    /// <summary>
    /// Display name of the recipient.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
