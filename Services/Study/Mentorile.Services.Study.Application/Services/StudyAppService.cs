using AutoMapper;
using Mentorile.Services.Study.Application.DTOs;
using Mentorile.Services.Study.Application.Interfaces;
using Mentorile.Services.Study.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Services;
public class StudyAppService : IStudyAppService
{
    private readonly IStudyService _studyService;
    private readonly IMapper _mapper;

    public StudyAppService(IStudyService studyService, IMapper mapper)
    {
        _studyService = studyService;
        _mapper = mapper;
    }

    public async Task<Result<StudyDTO>> CreateStudyAsync(CreateStudyDTO createStudyDTO)
    {
        var entity = _mapper.Map<Domain.Entities.Study>(createStudyDTO);
        var result = await _studyService.CreateStudyAsync(entity);
        return result.IsSuccess 
            ? Result<StudyDTO>.Success(_mapper.Map<StudyDTO>(result.Data))
            : Result<StudyDTO>.Failure(result.Message);
    }
    public async Task<Result<StudyDTO>> UpdateStudyAsync(UpdateStudyDTO updateStudyDTO)
    {
        var entity = _mapper.Map<Domain.Entities.Study>(updateStudyDTO);
        var result = await _studyService.UpdateStudyAsync(entity);
        return result.IsSuccess 
            ? Result<StudyDTO>.Success(_mapper.Map<StudyDTO>(result.Data))
            : Result<StudyDTO>.Failure(result.Message);
    }
    public async Task<Result<StudyDTO>> DeleteStudyAsync(string id)
    {
        var result = await _studyService.DeleteStudyAsync(id);
        return result.IsSuccess 
            ? Result<StudyDTO>.Success(_mapper.Map<StudyDTO>(result.Data))
            : Result<StudyDTO>.Failure(result.Message);
    }

    public async Task<Result<PagedResult<StudyDTO>>> GetAllAsync(PagingParams pagingParams)
    {
        var result = await _studyService.GetAllAsync(pagingParams);
        if (!result.IsSuccess) return Result<PagedResult<StudyDTO>>.Failure(result.Message);

        var studyDTOs = _mapper.Map<List<StudyDTO>>(result.Data.Data);
        var pagedResult = PagedResult<StudyDTO>.Create(studyDTOs, result.Data.TotalCount, pagingParams);
        return Result<PagedResult<StudyDTO>>.Success(pagedResult);
    }

    public async Task<Result<PagedResult<StudyDTO>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams)
    {
        var result = await _studyService.GetAllByUserIdAsync(userId, pagingParams);
        if (!result.IsSuccess) return Result<PagedResult<StudyDTO>>.Failure(result.Message);
        var studyDTOs = _mapper.Map<List<StudyDTO>>(result.Data.Data);
        var pagedResult = PagedResult<StudyDTO>.Create(studyDTOs, result.Data.TotalCount, pagingParams);
        return Result<PagedResult<StudyDTO>>.Success(pagedResult);
    }

    public async Task<Result<StudyDTO>> GetByIdAsync(string id)
    {
        var result = await _studyService.GetByIdAsync(id);
        return result.IsSuccess 
        ? Result<StudyDTO>.Success(_mapper.Map<StudyDTO>(result.Data))
        : Result<StudyDTO>.Failure(result.Message);
    }

    
}