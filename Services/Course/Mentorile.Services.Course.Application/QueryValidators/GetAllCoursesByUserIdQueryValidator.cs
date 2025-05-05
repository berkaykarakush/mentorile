using FluentValidation;
using Mentorile.Services.Course.Application.Queries;

namespace Mentorile.Services.Course.Application.QueryValidators;
public class GetAllCoursesByUserIdQueryValidator : AbstractValidator<GetAllCoursesByUserIdQuery>
{
    public GetAllCoursesByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required.");
    }
}