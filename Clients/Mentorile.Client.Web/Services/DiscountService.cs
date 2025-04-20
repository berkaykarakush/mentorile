using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Discounts;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;

namespace Mentorile.Client.Web.Services;
public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    private readonly ISharedIdentityService _sharedIdentityService;
    public DiscountService(HttpClient httpClient, ISharedIdentityService sharedIdentityService)
    {
        _httpClient = httpClient;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<DiscountViewModel> GetDiscountCodeAsync(string discountCode)
    {
        var response = await _httpClient.GetAsync($"discounts/{discountCode}/{_sharedIdentityService.GetUserId}");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<DiscountViewModel>>();
        return result.Data;
    }
}