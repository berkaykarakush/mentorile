using MediatR;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Application.Commands;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Email.Application.CommandHandlers;

public class SendManualEmailCommandHandler : IRequestHandler<SendManualEmailCommand, Result<bool>>
{
    private readonly IEmailSender _emailSender;

    public SendManualEmailCommandHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<Result<bool>> Handle(SendManualEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await _emailSender.SendEmailAsync(request.To, request.Subject, request.Body);
        if (!result.IsSuccess) return Result<bool>.Failure("Failed to send email.");
        return Result<bool>.Success(result.IsSuccess, "Email send successfully.");
    }
}