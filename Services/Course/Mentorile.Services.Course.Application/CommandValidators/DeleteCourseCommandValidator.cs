using FluentValidation;
using Mentorile.Services.Course.Application.Commands;

namespace Mentorile.Services.Course.Application.CommandValidators;
public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id field is required.");
    }
}