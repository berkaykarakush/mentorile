namespace Mentorile.Shared.Messages.Events;
public class UserRegisteredEvent
{
    public Guid UserId { get; set; }    
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreateAt { get; set; }
}