using AutoMapper;
using MediatR;
using Mentorile.Services.Study.Application.DTOs;
using Mentorile.Services.Study.Application.Queries;
using Mentorile.Services.Study.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.QueryHandlers;
public class GetAllStudiesQueryHandler : IRequestHandler<GetAllStudiesQuery, Result<PagedResult<StudyDTO>>>
{
    private readonly IStudyService _studyService;
    private readonly IMapper _mapper;

    public GetAllStudiesQueryHandler(IStudyService studyService, IMapper mapper)
    {
        _studyService = studyService;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<StudyDTO>>> Handle(GetAllStudiesQuery request, CancellationToken cancellationToken)
    {
        var studies = await _studyService.GetAllAsync(request.PagingParams);
        var studyDTOs = _mapper.Map<List<StudyDTO>>(studies.Data.Data);
        var pagedResult = PagedResult<StudyDTO>.Create(studyDTOs, studies.Data.TotalCount, request.PagingParams);
        return Result<PagedResult<StudyDTO>>.Success(pagedResult);
    }
}