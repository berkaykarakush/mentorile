using System.Net;
using System.Net.Mail;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Infrastructure.Settings;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Email.Infrastructure.Repository;

public class EmailSender : IEmailSender
{
    private readonly ISmtpSettings _smtpSettings;

    public EmailSender(ISmtpSettings smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public async Task<Result<bool>> SendEmailAsync(string to, string subject, string body)
    {
        using var smtp = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            EnableSsl = _smtpSettings.UseSsl,
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_smtpSettings.From),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mail.To.Add(to);

        await smtp.SendMailAsync(mail);
        return Result<bool>.Success(true);
    }
}