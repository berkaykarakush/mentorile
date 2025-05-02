using FluentValidation;
using Mentorile.Services.User.Application.Queries;

namespace Mentorile.Services.User.Application.QueryValidators;
public class SearchUsersQueryValidator : AbstractValidator<SearchUsersQuery>
{
    public SearchUsersQueryValidator()
    {
        RuleFor(x => x.Keyword)
            .NotEmpty().WithMessage("Search keyword is required.");
    }
}