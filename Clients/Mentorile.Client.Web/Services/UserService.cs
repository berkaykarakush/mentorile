using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
namespace Mentorile.Client.Web.Services;
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("get-all-users")]
    public async Task<List<UserViewModel>> GetAllUsers() => await _httpClient.GetFromJsonAsync<List<UserViewModel>>("/api/users/GetAllUsers");

    [HttpGet]
    public async Task<UserViewModel> GetUser() => await _httpClient.GetFromJsonAsync<UserViewModel>("/api/users/GetUser");
}