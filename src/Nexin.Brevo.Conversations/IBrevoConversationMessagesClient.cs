namespace Nexin.Brevo.Conversations;

public interface IBrevoConversationMessagesClient
{
    Task<BrevoConversationMessage> SendAgentMessageAsync(
        SendBrevoAgentMessageRequest request,
        CancellationToken cancellationToken = default);

    Task<BrevoConversationMessage> GetMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default);

    Task<BrevoConversationMessage> UpdateAgentMessageAsync(
        string messageId,
        UpdateBrevoMessageRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAgentMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default);
}
