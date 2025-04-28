using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Studies;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services;
public class StudyService : IStudyService
{
    private readonly HttpClient _httpClient;

    public StudyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    const string controllersName = "studies";
    public async Task<bool> CreateStudyAsync(CreateStudyInput createStudyInput)
    {
        var response = await _httpClient.PostAsJsonAsync($"{controllersName}/create", createStudyInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateStudyAsync(UpdateStudyInput updateStudyInput)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateStudyInput>($"{controllersName}/update", updateStudyInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteStudyAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{controllersName}/delete/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<PagedResult<StudyViewModel>> GetAllAsync(PagingParams pagingParams)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/get-all?pageNumber={pagingParams.PageNumber}&pageSize={pagingParams.PageSize}");
        if(!response.IsSuccessStatusCode) return null;
        var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<StudyViewModel>>>();

        return result.Data;
    }

    public async Task<PagedResult<StudyViewModel>> GetAllByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/user-get-all");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<StudyViewModel>>>();
        return result.Data;
    }

    public async Task<StudyViewModel> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/get-by-id/{id}");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<StudyViewModel>>();
        return result.Data;
    }
}