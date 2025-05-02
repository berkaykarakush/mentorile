using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Queries;
public class CheckPhoneNumberExistsQuery : IRequest<Result<bool>>
{
    public string PhoneNumber { get; set; }    
}