using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Domain.Services;
public interface IStudyService
{
    Task<Result<PagedResult<Entities.Study>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<Entities.Study>> GetByIdAsync(string id);
    Task<Result<PagedResult<Entities.Study>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams);
    Task<Result<Entities.Study>> CreateStudyAsync(Entities.Study study);
    Task<Result<Entities.Study>> UpdateStudyAsync(Entities.Study study);
    Task<Result<Entities.Study>> DeleteStudyAsync(string id);
}