using FluentValidation;
using Mentorile.Services.User.Application.Queries;

namespace Mentorile.Services.User.Application.QueryValidators;
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required.");
    }
}