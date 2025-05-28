using System.Text.RegularExpressions;
using Mentorile.Services.Email.Application.Services.Abstractions;
using Mentorile.Services.Email.Domain.Enums;
using Mentorile.Services.Email.Domain.Interfaces;

namespace Mentorile.Services.Email.Application.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateLoader _loader;

    public EmailTemplateService(IEmailTemplateLoader loader)
    {
        _loader = loader;
    }

    public async Task<(string subject, string body)> GenerateEmailAsync(EmailTemplateType type, Dictionary<string, string> placeholders)
    {
        var subject = await _loader.LoadTemplateSubjectAsync(type);
        var body = await _loader.LoadTemplateBodyAsync(type);

        body = ReplacePlaceholders(body, placeholders);
        subject = ReplacePlaceholders(subject, placeholders);
        
        return (subject, body);
    }

    private string ReplacePlaceholders(string input, Dictionary<string, string> placeholders)
    {
        return Regex.Replace(input, @"\{\{(\w+)\}\}", match =>
        {
            var key = match.Groups[1].Value;
            return placeholders.TryGetValue(key, out var value) ? value : match.Value;
        });
    }
}