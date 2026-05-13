namespace Nexin.Brevo.Conversations;

internal sealed class BrevoAutomatedMessagesClient : IBrevoAutomatedMessagesClient
{
    private readonly BrevoConversationsHttpClient _client;

    public BrevoAutomatedMessagesClient(BrevoConversationsHttpClient client)
    {
        _client = client;
    }

    public Task<BrevoConversationMessage> SendAutomatedMessageAsync(
        SendBrevoAutomatedMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        return _client.PostAsync<SendBrevoAutomatedMessageRequest, BrevoConversationMessage>(
            "/v3/conversations/pushedMessages",
            request,
            cancellationToken);
    }

    public Task<BrevoConversationMessage> GetAutomatedMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.GetAsync<BrevoConversationMessage>(
            $"/v3/conversations/pushedMessages/{Uri.EscapeDataString(messageId)}",
            cancellationToken);
    }

    public Task<BrevoConversationMessage> UpdateAutomatedMessageAsync(
        string messageId,
        UpdateBrevoMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.PutAsync<UpdateBrevoMessageRequest, BrevoConversationMessage>(
            $"/v3/conversations/pushedMessages/{Uri.EscapeDataString(messageId)}",
            request,
            cancellationToken);
    }

    public Task DeleteAutomatedMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.DeleteAsync(
            $"/v3/conversations/pushedMessages/{Uri.EscapeDataString(messageId)}",
            cancellationToken);
    }
}
