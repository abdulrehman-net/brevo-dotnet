using System.Text.Json.Serialization;

namespace Abdul.Brevo.Conversations;

public sealed class SetBrevoVisitorGroupRequest
{
    [JsonPropertyName("groupId")]
    public required string GroupId { get; init; }
}

public sealed class BrevoVisitorGroupAssignmentResponse
{
    [JsonPropertyName("groupId")]
    public string? GroupId { get; init; }

    [JsonPropertyName("visitorId")]
    public string? VisitorId { get; init; }
}
