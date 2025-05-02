using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Handlers;
public class RegisterUserCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
}