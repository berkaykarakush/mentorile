using FluentValidation;
using Mentorile.Services.Course.Application.Queries;

namespace Mentorile.Services.Course.Application.QueryValidators;
public class GetCourseByIdQueryValidator : AbstractValidator<GetCourseByIdQuery>
{
    public GetCourseByIdQueryValidator()
    {
          RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}