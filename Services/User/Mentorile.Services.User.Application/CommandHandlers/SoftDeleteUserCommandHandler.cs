using MediatR;
using Mentorile.Services.User.Application.Commands;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.CommandHandlers;
public class SoftDeleteUserCommandHandler : IRequestHandler<SoftDeleteUserCommand, Result<bool>>
{
    private readonly IUserRepository _userRepository;

    public SoftDeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.SoftDeleteAsync(request.UserId);
        if (!userResult.IsSuccess) return Result<bool>.Failure("Failed to user delete");
        return Result<bool>.Success(userResult.Data, "User deleted successfully.");
    }
}