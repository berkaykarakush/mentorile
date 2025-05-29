namespace Mentorile.Shared.Messages.Responses;
public class UserAccessCheckResponse
{
    public bool IsAccessGranted { get; set; }
    /// <summary>
    /// Example: EmailNotConfirmed, SubscriptionExpired 
    /// </summary>
    public string Reason { get; set; } = default!;
}