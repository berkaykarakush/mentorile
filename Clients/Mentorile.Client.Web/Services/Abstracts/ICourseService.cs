using Mentorile.Client.Web.ViewModels.Courses;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface ICourseService
{
    Task<List<CourseViewModel>> GetAllCourseAsync();
    Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
    Task<CourseViewModel> GetCourseByIdAsync(string courseId);
    Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput);
    Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput);
    Task<bool> DeleteCourseAsync(string courseId);  
}