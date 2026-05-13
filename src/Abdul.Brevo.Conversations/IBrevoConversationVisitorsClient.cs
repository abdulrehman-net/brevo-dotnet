namespace Abdul.Brevo.Conversations;

public interface IBrevoConversationVisitorsClient
{
    Task<BrevoVisitorGroupAssignmentResponse> SetVisitorGroupAsync(
        SetBrevoVisitorGroupRequest request,
        CancellationToken cancellationToken = default);
}
