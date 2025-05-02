using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Queries;
public class GetUserByEmailQuery : IRequest<Result<UserDTO>>
{
    public string Email { get; set; }
}