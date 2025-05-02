using MediatR;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Handlers;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterAsync(request.Name, request.Surname, request.Email, request.PhoneNumber, request.Password);
        if(!response.IsSuccess) return Result<Guid>.Failure("Failed to user registration.");
        return Result<Guid>.Success(response.Data, "User registered successfully.");
    }
}