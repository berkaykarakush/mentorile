using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Queries;
public class CheckEmailExistsQuery : IRequest<Result<bool>>
{
    public string Email { get; set; }    
}