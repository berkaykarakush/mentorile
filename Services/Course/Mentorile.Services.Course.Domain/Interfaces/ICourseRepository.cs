using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Domain.Interfaces;
public interface ICourseRepository
{
    Task<Result<PagedResult<Entities.Course>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<Entities.Course>> GetByIdAsync(string id);   
    Task<Result<PagedResult<Entities.Course>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams);
    Task<Result<Entities.Course>> CreateCourseAsync(Entities.Course course);
    Task<Result<bool>> UpdateCourseAsync(Entities.Course course);
    Task<Result<bool>> DeleteAsync(string id);
}