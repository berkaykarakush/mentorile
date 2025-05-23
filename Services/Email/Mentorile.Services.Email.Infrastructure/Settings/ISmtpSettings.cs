namespace Mentorile.Services.Email.Infrastructure.Settings;
public interface ISmtpSettings
{
    // Gmail, Hotmail, vs.
    public string Provider { get; set; }    
    public string Host { get; set; }
    public int Port { get; set; }
    public string From { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool UseSsl { get; set; }
}