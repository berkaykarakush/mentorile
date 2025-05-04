using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Studies;
using Mentorile.Client.Web.ViewModels.Users;
using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Mvc;
namespace Mentorile.Client.Web.Services;
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private const string controllersName = "users";
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<List<UserViewModel>> GetAllUsers() 
        => await _httpClient.GetFromJsonAsync<List<UserViewModel>>($"{controllersName}");

    [HttpGet("active")]
    public async Task<List<UserViewModel>> GetAllActiveUsers() 
    {
        var response = await _httpClient.GetAsync($"{controllersName}");
        if(!response.IsSuccessStatusCode) return new List<UserViewModel>();
        var result = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
        return result;
    }

    [HttpGet("get-user")]
    public async Task<UserViewModel> GetUser(string userId)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/by-id?userId={userId}");
        if(!response.IsSuccessStatusCode) return new UserViewModel();

        var result = await response.Content.ReadFromJsonAsync<Result<UserViewModel>>();
        return result.Data;
    }
}