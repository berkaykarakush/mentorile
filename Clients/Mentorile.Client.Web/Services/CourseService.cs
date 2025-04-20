using Mentorile.Client.Web.Helpers;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Courses;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services;
public class CourseService : ICourseService
{
    private readonly HttpClient _httpClient;
    private readonly IPhotoStockService _photoStockService;
    private readonly PhotoHelper _photoHelper;

    public CourseService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
    {
        _httpClient = httpClient;
        _photoStockService = photoStockService;
        _photoHelper = photoHelper;
    }

    public async Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
    {
        var resultPhotoService = await _photoStockService.UploadPhotoAsync(createCourseInput.PhotoFormFile);
        if(resultPhotoService != null)
        {
            createCourseInput.PhotoUri = resultPhotoService.Data.PhotoUri;
        }
        var response = await _httpClient.PostAsJsonAsync<CreateCourseInput>("courses", createCourseInput);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _httpClient.DeleteAsync($"courses/{courseId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        // http://localhost:5000/services/course/courses
        var response = await _httpClient.GetAsync("courses");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<List<CourseViewModel>>>();
        result.Data.ForEach(x => {
            x.PhotoUri = _photoHelper.GetPhotoStockUri(x.PhotoUri);
        });

        return result.Data;
    }

    public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
    {
        // [controller]/GetAllByUserId/{userId}
        var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<List<CourseViewModel>>>();
        result.Data.ForEach(x => {
            x.PhotoUri = _photoHelper.GetPhotoStockUri(x.PhotoUri);
        });
        return result.Data;
    }

    public async Task<CourseViewModel> GetCourseByIdAsync(string courseId)
    {
        var response = await _httpClient.GetAsync($"courses/{courseId}");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<CourseViewModel>>();
        return result.Data;
    }

    public async Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
    {
        var resultPhotoService = await _photoStockService.UploadPhotoAsync(updateCourseInput.PhotoFormFile);
        if(resultPhotoService != null)
        {
            await _photoStockService.DeletePhotoAsync(updateCourseInput.PhotoUri);
            updateCourseInput.PhotoUri = resultPhotoService.Data.PhotoUri;
        }
        var response = await _httpClient.PutAsJsonAsync<UpdateCourseInput>("courses", updateCourseInput);
        return response.IsSuccessStatusCode;
    }
}