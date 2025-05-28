using Mentorile.Services.Email.Domain.Enums;
using Mentorile.Services.Email.Domain.Interfaces;

namespace Mentorile.Services.Email.Infrastructure.Repository;

public class EmailTemplateLoader : IEmailTemplateLoader
{
    private readonly string _templateFolderPath;

    public EmailTemplateLoader()
        => _templateFolderPath = Path.Combine(AppContext.BaseDirectory, "Templates");

    public async Task<string> LoadTemplateBodyAsync(EmailTemplateType type)
        => await File.ReadAllTextAsync(Path.Combine(_templateFolderPath, $"{type}.html"));

    public Task<string> LoadTemplateSubjectAsync(EmailTemplateType type)
        => Task.FromResult(GetDefaultSubject(type));

    private string GetDefaultSubject(EmailTemplateType type) => type switch
    {
        EmailTemplateType.AccountConfirmation => "Hesabınızı Onaylayın",
        EmailTemplateType.PasswordReset => "Şifre Sıfırlama Talebiniz",
        EmailTemplateType.CampaignAnnoucement => "Yeni Kampanyamız Hakkında",
        EmailTemplateType.OrderReceived => "Siparişiniz alındı",
        _ => "Mentorile.com"
    };
}