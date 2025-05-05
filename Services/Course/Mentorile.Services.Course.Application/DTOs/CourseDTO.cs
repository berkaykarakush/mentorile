namespace Mentorile.Services.Course.Application.DTOs;
public class CourseDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool? IsDeleted { get; set; }
    public string PhotoUri { get; set; } = string.Empty;
    public List<string> TopicIds { get; set; } = new();
}