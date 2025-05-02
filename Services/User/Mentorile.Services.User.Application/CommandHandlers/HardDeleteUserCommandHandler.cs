using MediatR;
using Mentorile.Services.User.Application.Commands;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.CommandHandlers;
public class HardDeleteUserCommandHandler : IRequestHandler<HardDeleteUserCommand, Result<bool>>
{
    private readonly IUserRepository _userRepository;

    public HardDeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(HardDeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.HardDeleteAsync(request.UserId);
        if (!userResult.IsSuccess) return Result<bool>.Failure("User not deleted."); 
        return Result<bool>.Success(userResult.Data, "User deleted successfully.");
    }
}