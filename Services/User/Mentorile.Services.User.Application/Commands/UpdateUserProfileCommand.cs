using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Commands;
public class UpdateUserProfileCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}