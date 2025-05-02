using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Queries;
public class GetUserByIdQuery : IRequest<Result<UserDTO>>
{
    public Guid UserId { get; set; }
}