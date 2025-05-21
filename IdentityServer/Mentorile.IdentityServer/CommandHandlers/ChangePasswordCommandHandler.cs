using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.CommandHandlers;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}