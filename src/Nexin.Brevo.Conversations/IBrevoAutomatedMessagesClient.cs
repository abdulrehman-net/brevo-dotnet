namespace Nexin.Brevo.Conversations;

public interface IBrevoAutomatedMessagesClient
{
    Task<BrevoConversationMessage> SendAutomatedMessageAsync(
        SendBrevoAutomatedMessageRequest request,
        CancellationToken cancellationToken = default);

    Task<BrevoConversationMessage> GetAutomatedMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default);

    Task<BrevoConversationMessage> UpdateAutomatedMessageAsync(
        string messageId,
        UpdateBrevoMessageRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAutomatedMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default);
}
