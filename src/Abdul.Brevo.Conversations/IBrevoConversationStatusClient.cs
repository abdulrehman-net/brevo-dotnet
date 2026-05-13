namespace Abdul.Brevo.Conversations;

public interface IBrevoConversationStatusClient
{
    Task SetAgentOnlineAsync(
        SetBrevoAgentOnlineRequest request,
        CancellationToken cancellationToken = default);
}
