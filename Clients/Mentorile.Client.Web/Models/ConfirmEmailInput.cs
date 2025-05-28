namespace Mentorile.Client.Web.Models;
public class ConfirmEmailInput
{
    public string UserId { get; set; }    
    public string ConfirmationCode { get; set; }
}