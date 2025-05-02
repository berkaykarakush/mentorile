using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Commands;
public class HardDeleteUserCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }    
}