using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.IdentityServer.DTOs;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.CommandHandlers;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserAuthenticatedDto>>
{
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<UserAuthenticatedDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterAsync(request.Name, request.Surname, request.Email, request.PhoneNumber, request.Password);
        if(!response.IsSuccess) return Result<UserAuthenticatedDto>.Failure("Failed to user registration.");
        return Result<UserAuthenticatedDto>.Success(response.Data, "User registered successfully.");
    }
}