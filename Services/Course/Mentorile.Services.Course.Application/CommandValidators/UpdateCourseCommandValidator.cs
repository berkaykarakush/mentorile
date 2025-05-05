using System.Data;
using FluentValidation;
using Mentorile.Services.Course.Application.Commands;

namespace Mentorile.Services.Course.Application.CommandValidators;
public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Id field is required.");

        RuleFor(x => x.Name)   
            .NotEmpty().WithMessage("Name field is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id field is required.");
    }
}