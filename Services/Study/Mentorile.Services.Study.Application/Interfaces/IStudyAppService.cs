using Mentorile.Services.Study.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Interfaces;

public interface IStudyAppService
{
    Task<Result<PagedResult<StudyDTO>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<StudyDTO>> GetByIdAsync(string id);
    Task<Result<PagedResult<StudyDTO>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams);
    Task<Result<StudyDTO>> CreateStudyAsync(CreateStudyDTO createStudyDTO);
    Task<Result<StudyDTO>> UpdateStudyAsync(UpdateStudyDTO updateStudyDTO);
    Task<Result<StudyDTO>> DeleteStudyAsync(string id);
}