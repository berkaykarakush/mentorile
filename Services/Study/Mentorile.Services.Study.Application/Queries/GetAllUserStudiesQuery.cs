using MediatR;
using Mentorile.Services.Study.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Queries;
public class GetAllUserStudiesQuery: IRequest<Result<PagedResult<StudyDTO>>>
{
    public string UserId { get; set; }
    public PagingParams PagingParams { get; set; }    
}