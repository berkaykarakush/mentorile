namespace Mentorile.Services.Study.Domain.Entities;
public class Study
{
    public string Id { get; set; } = Guid.NewGuid().ToString();  
    public string Name { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedDate { get; set; } 
}