namespace Abdul.Brevo.Conversations;

internal sealed class BrevoConversationStatusClient : IBrevoConversationStatusClient
{
    private readonly BrevoConversationsHttpClient _client;

    public BrevoConversationStatusClient(BrevoConversationsHttpClient client)
    {
        _client = client;
    }

    public Task SetAgentOnlineAsync(
        SetBrevoAgentOnlineRequest request,
        CancellationToken cancellationToken = default)
    {
        return _client.PostNoContentAsync(
            "/v3/conversations/agentOnlinePing",
            request,
            cancellationToken);
    }
}
