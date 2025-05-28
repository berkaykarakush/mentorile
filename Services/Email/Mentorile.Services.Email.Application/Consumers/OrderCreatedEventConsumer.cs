using MassTransit;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Shared.Messages.Events;

namespace Mentorile.Services.Email.Application.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailUserRepository _emailUserRepository;

    public OrderCreatedEventConsumer(IEmailSender emailSender, IEmailUserRepository emailUserRepository)
    {
        _emailSender = emailSender;
        _emailUserRepository = emailUserRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var user = await _emailUserRepository.GetByIdAsync(context.Message.BuyerId);
        if (user == null) return;

        var to = user.Data.Email;
        var subject = $"Siparişiniz alındı. - {context.Message.OrderId}";
        var body = $"Sayın {user.Data.Name},\n\nSiparişiniz başarıyla alındı. Sipariş ID: {context.Message.OrderId}";

        await _emailSender.SendEmailAsync(to, subject, body);
    }
}