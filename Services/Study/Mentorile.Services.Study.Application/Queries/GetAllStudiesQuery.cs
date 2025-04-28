using MediatR;
using Mentorile.Services.Study.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Queries;
public class GetAllStudiesQuery : IRequest<Result<PagedResult<StudyDTO>>>
{
    public PagingParams PagingParams { get; set; }
}