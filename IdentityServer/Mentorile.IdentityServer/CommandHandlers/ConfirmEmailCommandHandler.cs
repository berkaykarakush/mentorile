using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.CommandHandlers;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<bool>>
{
    private readonly IAuthService _authService;

    public ConfirmEmailCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.ConfirmEmailAsync(request.UserId, request.ConfirmationCode);
        if (!response.IsSuccess) return Result<bool>.Failure("Failed to email confirmation.");
        return Result<bool>.Success(response.Data, "Email confirmation successfully.");
    }
}