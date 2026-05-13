namespace Nexin.Brevo.Conversations;

public sealed class SetBrevoAgentOnlineRequest
{
    public string? AgentId { get; init; }

    public string? AgentEmail { get; init; }

    public string? AgentName { get; init; }

    public string? ReceivedFrom { get; init; }
}
