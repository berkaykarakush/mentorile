using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Commands;
public class LoginUserCommand : IRequest<Result<bool>>
{

}