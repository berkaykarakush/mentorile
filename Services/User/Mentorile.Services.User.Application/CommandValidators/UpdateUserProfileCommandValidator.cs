using FluentValidation;
using Mentorile.Services.User.Application.Commands;

namespace Mentorile.Services.User.Application.CommandValidators;
public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required");;
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches("^\\+90[0-9]{10}$").WithMessage("A valid phone number is required");
    }
}