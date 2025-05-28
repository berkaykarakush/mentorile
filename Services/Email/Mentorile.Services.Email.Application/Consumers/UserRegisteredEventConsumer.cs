using MassTransit;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Application.Services.Abstractions;
using Mentorile.Services.Email.Domain.Entities;
using Mentorile.Services.Email.Domain.Enums;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Shared.Messages.Events;
using Microsoft.AspNetCore.Builder.Extensions;

namespace Mentorile.Services.Email.Application.Consumers;

public class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IEmailUserRepository _emailUserRepository;
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateService _emailTemplateService;

    public UserRegisteredEventConsumer(IEmailUserRepository emailUserRepository, IEmailSender emailSender, IEmailTemplateService emailTemplateService)
    {
        _emailUserRepository = emailUserRepository;
        _emailSender = emailSender;
        _emailTemplateService = emailTemplateService;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var user = new EmailUser()
        {
            UserId = context.Message.UserId.ToString(),
            Name = context.Message.Name,
            Surname = context.Message.Surname,
            Email = context.Message.Email
        };

        await _emailUserRepository.AddAsync(user);
        var username = $"{context.Message.Name} {context.Message.Surname}";

        var expireAt = context.Message.EmailConfirmationCodeExpireAt?.ToString("dd-MM-yyyy HH:mm");
        var placeholders = new Dictionary<string, string>
        {
            { "UserName", username},
            { "ConfirmationCode", context.Message.ConfirmationCode},
            { "ExpireAt", expireAt},
        };

        var (subject, body) = await _emailTemplateService.GenerateEmailAsync(EmailTemplateType.AccountConfirmation, placeholders);
        await _emailSender.SendEmailAsync(context.Message.Email, subject, body);
    }
}