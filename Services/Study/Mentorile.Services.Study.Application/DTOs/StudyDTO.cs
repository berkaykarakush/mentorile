namespace Mentorile.Services.Study.Application.DTOs;
public class StudyDTO
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedDate { get; set; }        
}