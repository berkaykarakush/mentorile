using FluentValidation;
using Mentorile.Services.Payment.Application.Commands;

namespace Mentorile.Services.Payment.Application.CommandValidators;
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        
    }
}