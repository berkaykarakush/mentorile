using FluentValidation;
using Mentorile.Services.Payment.Application.Commands;

namespace Mentorile.Services.Payment.Application.CommandValidators;
public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        
    }
}