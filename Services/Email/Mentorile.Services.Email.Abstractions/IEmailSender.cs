using Mentorile.Shared.Common;
namespace Mentorile.Services.Email.Abstractions;
public interface IEmailSender
{
    Task<Result<bool>> SendEmailAsync(string  to, string subject, string body);    
}