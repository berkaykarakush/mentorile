using MediatR;
using Mentorile.Services.User.Application.Queries;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.QueryHandlers;
public class CheckEmailExistsQueryHandler : IRequestHandler<CheckEmailExistsQuery, Result<bool>>
{
    private readonly IUserRepository _userRepository;

    public CheckEmailExistsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(CheckEmailExistsQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.EmailExistsAsync(request.Email);
        if (!userResult.IsSuccess) return Result<bool>.Failure("Could not check email existence.");
        return Result<bool>.Success(userResult.Data, userResult.Data ? "Email is exists." : "Email not found.");
    }
}