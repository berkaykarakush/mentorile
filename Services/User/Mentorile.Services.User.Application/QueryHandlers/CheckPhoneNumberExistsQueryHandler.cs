using MediatR;
using Mentorile.Services.User.Application.Queries;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.QueryHandlers;
public class CheckPhoneNumberExistsQueryHandler : IRequestHandler<CheckPhoneNumberExistsQuery, Result<bool>>
{
    private readonly IUserRepository _userRepository;

    public CheckPhoneNumberExistsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(CheckPhoneNumberExistsQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.PhoneNumberExistsAsync(request.PhoneNumber);
        if(!userResult.IsSuccess) return Result<bool>.Failure("Could not check phone number existence.");
        return Result<bool>.Success(userResult.Data, userResult.Data ? "Phone number is exists." : "Phone number not found.");
    }
}