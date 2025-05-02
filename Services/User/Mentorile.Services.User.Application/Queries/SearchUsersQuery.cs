using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.Queries;
public class SearchUsersQuery : IRequest<Result<PagedResult<UserDTO>>>
{
    public string Keyword { get; set; }    
    public PagingParams PagingParams { get; set; }    
}