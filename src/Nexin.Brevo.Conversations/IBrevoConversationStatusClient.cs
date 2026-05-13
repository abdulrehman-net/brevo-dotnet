namespace Nexin.Brevo.Conversations;

public interface IBrevoConversationStatusClient
{
    Task SetAgentOnlineAsync(
        SetBrevoAgentOnlineRequest request,
        CancellationToken cancellationToken = default);
}
