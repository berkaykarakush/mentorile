using FluentValidation;
using Mentorile.Services.Email.Application.Commands;

namespace Mentorile.Services.Email.Application.CommandValidators;
public class SendManualCommandValidator : AbstractValidator<SendManualEmailCommand>
{
    public SendManualCommandValidator()
    {
        RuleFor(x => x.To)
            .NotEmpty().WithMessage("To field is required.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject field is required.");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body field is required.");
    }    
}