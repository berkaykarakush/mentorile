using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.CommandHandlers;

public class ResendConfirmEmailCommandHandler : IRequestHandler<ResendConfirmEmailCommand, Result<bool>>
{
    private readonly IAuthService _authService;

    public ResendConfirmEmailCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<bool>> Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.ResendConfirmEmailAsync(request.UserId);
        if (!result.IsSuccess) return Result<bool>.Failure("Failed to email confirmation.");
        return Result<bool>.Success(result.Data, "Email confirmation successfully.");
    }
}