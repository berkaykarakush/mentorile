namespace Mentorile.Services.User.Application.DTOs;
public class UserDTO
{
    public Guid UserId { get; set; }    
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}