using System.Text.Json.Serialization;

namespace Abdul.Brevo.Email.Models;

/// <summary>
/// Scheduled email information returned from <c>GET /v3/smtp/emails/{identifier}</c>.
/// </summary>
public sealed class ScheduledEmailInfo
{
    [JsonPropertyName("count")]
    public long? Count { get; set; }

    [JsonPropertyName("batches")]
    public List<ScheduledBatch>? Batches { get; set; }
}

/// <summary>
/// A single scheduled email batch.
/// </summary>
public sealed class ScheduledBatch
{
    [JsonPropertyName("scheduledAt")]
    public DateTimeOffset? ScheduledAt { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
