using Mentorile.Services.Email.Domain.Enums;

namespace Mentorile.Services.Email.Domain.Interfaces;
public interface IEmailTemplateLoader
{
    Task<string> LoadTemplateBodyAsync(EmailTemplateType type);
    Task<string> LoadTemplateSubjectAsync(EmailTemplateType type); 
}