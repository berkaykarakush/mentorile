using System.Net;
using System.Net.Mail;
using System.Text;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Services.Email.Infrastructure.Settings;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Email.Infrastructure.Repository;

public class EmailSender : IEmailSender
{
    private readonly ISmtpSettings _smtpSettings;
    private readonly IEmailLogRepository _emailLogRepository;

    public EmailSender(ISmtpSettings smtpSettings, IEmailLogRepository emailLogRepository)
    {
        _smtpSettings = smtpSettings;
        _emailLogRepository = emailLogRepository;
    }

    public async Task<Result<bool>> SendEmailAsync(string to, string subject, string body)
    {
         var mail = new MailMessage
        {
            From = new MailAddress(_smtpSettings.From, "Mentorile.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
            BodyEncoding = Encoding.UTF8,
            SubjectEncoding = Encoding.UTF8
        };
        mail.To.Add(to);
        try
        {
            var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                UseDefaultCredentials = false,
                EnableSsl = _smtpSettings.UseSsl
            };

            Console.WriteLine("SMTP client created, sending...");
            await client.SendMailAsync(mail);
            Console.WriteLine("Mail sent successfully");
            await _emailLogRepository.AddAsync(new Domain.Entities.EmailLog
            {
                To = to,
                Subject = subject,
                Body = body,
                IsSuccess = true,
            });
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SMTP ERROR: {ex.Message}");
            await _emailLogRepository.AddAsync(new Domain.Entities.EmailLog
            {
                To = to,
                Subject = subject,
                Body = body,
                IsSuccess = false,
                ErrorMessage = ex.Message
            });

            return Result<bool>.Failure("Failed to mail sending.");
        }
    }
}