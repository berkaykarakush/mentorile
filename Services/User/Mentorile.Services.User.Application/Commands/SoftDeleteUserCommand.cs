using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Commands;
public class SoftDeleteUserCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }       
}