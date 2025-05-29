using System.Text.Json;
using Mentorile.Gateway.API.Models;
using Mentorile.Gateway.API.Services.Abstracts;

namespace Mentorile.Gateway.API.Services;
public class UserAccessService : IUserAccessService
{
    private readonly HttpClient _httpClient;

    public UserAccessService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserAccessProfile> GetUserAccessProfileAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"/api/users/{userId}/access-profile");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserAccessProfile>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
