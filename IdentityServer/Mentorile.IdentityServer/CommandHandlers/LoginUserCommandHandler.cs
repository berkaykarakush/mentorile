using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.CommandHandlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}