using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.CommandHandlers;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    private readonly IAuthService _authService;

    public ChangePasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);
        if (!result.IsSuccess) return Result<bool>.Failure("Failed to password changed.");
        return Result<bool>.Success(result.Data, "Password changed successfully.");
    }
}