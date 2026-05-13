namespace Abdul.Brevo.Conversations;

internal sealed class BrevoConversationVisitorsClient : IBrevoConversationVisitorsClient
{
    private readonly BrevoConversationsHttpClient _client;

    public BrevoConversationVisitorsClient(BrevoConversationsHttpClient client)
    {
        _client = client;
    }

    public Task<BrevoVisitorGroupAssignmentResponse> SetVisitorGroupAsync(
        SetBrevoVisitorGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        return _client.PutAsync<SetBrevoVisitorGroupRequest, BrevoVisitorGroupAssignmentResponse>(
            "/v3/conversations/visitorGroup",
            request,
            cancellationToken);
    }
}
