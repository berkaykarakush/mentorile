using Mentorile.Services.Email.Domain.Enums;

namespace Mentorile.Services.Email.Domain.Entities;

public class EmailTemplate
{
    public EmailTemplateType Type { get; set; }
    public string SubjectTemplate { get; set; } = default!;
    public string BodyTemplate { get; set; } = default!;
}