using Mentorile.Services.Email.Domain.Enums;

namespace Mentorile.Services.Email.Application.Services.Abstractions;
public interface IEmailTemplateService
{
    Task<(string subject, string body)> GenerateEmailAsync(EmailTemplateType type, Dictionary<string, string> placeholders);    
}