using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Queries;
public class GetAllActiveUersQuery : IRequest<Result<PagedResult<UserDTO>>>
{
    public PagingParams PagingParams { get; set; }    
}