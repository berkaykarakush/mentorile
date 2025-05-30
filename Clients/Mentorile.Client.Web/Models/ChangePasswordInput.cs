namespace Mentorile.Client.Web.Models;

public class ChangePasswordInput
{
    public string UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    // TODO: Identity kisminda re new password unuttuk eklenecek
    public string ReNewPassword { get; set; }
}