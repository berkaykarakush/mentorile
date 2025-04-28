using Mentorile.Client.Web.ViewModels.Studies;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IStudyService
{
    Task<PagedResult<StudyViewModel>> GetAllAsync(PagingParams pagingParams);
    Task<PagedResult<StudyViewModel>> GetAllByUserIdAsync(string userId);
    Task<StudyViewModel> GetByIdAsync(string id);
    Task<bool> CreateStudyAsync(CreateStudyInput createStudyInput);
    Task<bool> UpdateStudyAsync(UpdateStudyInput updateStudyInput);
    Task<bool> DeleteStudyAsync(string id);
}