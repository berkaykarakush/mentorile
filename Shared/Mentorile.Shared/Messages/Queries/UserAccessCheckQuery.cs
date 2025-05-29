namespace Mentorile.Shared.Messages.Queries;
public class UserAccessCheckQuery
{
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Example: course_detail  
    /// </summary>
    public string Target { get; set; } = default!;
}