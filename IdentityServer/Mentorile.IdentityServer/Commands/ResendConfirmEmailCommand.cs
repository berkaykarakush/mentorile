using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Commands;
public class ResendConfirmEmailCommand : IRequest<Result<bool>>
{
    public string UserId { get; set; }    
}