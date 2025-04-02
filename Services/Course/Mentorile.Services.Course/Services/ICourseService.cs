using Mentorile.Services.Course.DTOs.Course;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Services;
public interface ICourseService
{
    Task<Result<List<CourseDTO>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<CourseDTO>> GetByIdAsync(string id);   
    Task<Result<List<CourseDTO>>> GetAllByUserIdAsync(string userId);
    Task<Result<CourseDTO>> CreateCourseAsync(CreateCourseDTO createCourseDTO);
    Task<Result<CourseDTO>> UpdateCourseAsync(UpdateCourseDTO updateCourseDTO);
    Task<Result<CourseDTO>> DeleteAsync(string id);
}