namespace Abdul.Brevo.Conversations;

internal sealed class BrevoConversationMessagesClient : IBrevoConversationMessagesClient
{
    private readonly BrevoConversationsHttpClient _client;

    public BrevoConversationMessagesClient(BrevoConversationsHttpClient client)
    {
        _client = client;
    }

    public Task<BrevoConversationMessage> SendAgentMessageAsync(
        SendBrevoAgentMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateAgentIdentity(
            request.AgentId,
            request.AgentEmail,
            request.AgentName,
            request.ReceivedFrom);

        return _client.PostAsync<SendBrevoAgentMessageRequest, BrevoConversationMessage>(
            "/v3/conversations/messages",
            request,
            cancellationToken);
    }

    public Task<BrevoConversationMessage> GetMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.GetAsync<BrevoConversationMessage>(
            $"/v3/conversations/messages/{Uri.EscapeDataString(messageId)}",
            cancellationToken);
    }

    public Task<BrevoConversationMessage> UpdateAgentMessageAsync(
        string messageId,
        UpdateBrevoMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.PutAsync<UpdateBrevoMessageRequest, BrevoConversationMessage>(
            $"/v3/conversations/messages/{Uri.EscapeDataString(messageId)}",
            request,
            cancellationToken);
    }

    public Task DeleteAgentMessageAsync(
        string messageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageId);

        return _client.DeleteAsync(
            $"/v3/conversations/messages/{Uri.EscapeDataString(messageId)}",
            cancellationToken);
    }

    private static void ValidateAgentIdentity(
        string? agentId,
        string? agentEmail,
        string? agentName,
        string? receivedFrom)
    {
        var hasAgentId = !string.IsNullOrWhiteSpace(agentId);

        var hasExternalAgent =
            !string.IsNullOrWhiteSpace(agentEmail)
            && !string.IsNullOrWhiteSpace(agentName)
            && !string.IsNullOrWhiteSpace(receivedFrom);

        if (!hasAgentId && !hasExternalAgent)
        {
            throw new ArgumentException(
                "Provide either agentId, or all three values: agentEmail, agentName, and receivedFrom.");
        }
    }
}
