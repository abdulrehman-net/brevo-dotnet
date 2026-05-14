using System.Text.Json.Serialization;

namespace Abdul.Brevo.Core.Models;

/// <summary>
/// Represents a webhook configuration in Brevo.
/// </summary>
public sealed class BrevoWebhook
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("events")]
    public IReadOnlyList<string>? Events { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("modifiedAt")]
    public DateTimeOffset ModifiedAt { get; init; }
}

public sealed class GetWebhooksRequest
{
    /// <summary>
    /// Filter on webhook type (transactional or marketing).
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// Sort the results in the ascending/descending order of webhook creation. Default is desc.
    /// </summary>
    public string? Sort { get; set; }

    public string AppendTo(string path)
    {
        var url = path;
        var hasQuery = false;

        if (!string.IsNullOrWhiteSpace(Type))
        {
            url += $"?type={Type.ToLowerInvariant()}";
            hasQuery = true;
        }

        if (!string.IsNullOrWhiteSpace(Sort))
        {
            var separator = hasQuery ? "&" : "?";
            url += $"{separator}sort={Sort.ToLowerInvariant()}";
        }

        return url;
    }
}

public sealed class CreateWebhookRequest
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("events")]
    public IReadOnlyList<string> Events { get; set; } = [];

    /// <summary>
    /// "transactional" or "marketing"
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }
}

public sealed class UpdateWebhookRequest
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("events")]
    public IReadOnlyList<string>? Events { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }
}
