namespace Mentorile.Services.Email.Infrastructure.Settings;
public class SmtpSettings : ISmtpSettings
{
    // Gmail, Hotmail, vs.
    public string Provider { get; set; } = default!;    
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string From { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool UseSsl { get; set; }
}