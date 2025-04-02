namespace Mentorile.Services.Course.DTOs.Course;
public class CourseDTO
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public string UserId { get; set; }

    public DateTime CreatedTime { get; set; }

    public List<string> TopicIds { get; set; } = new List<string>();
}