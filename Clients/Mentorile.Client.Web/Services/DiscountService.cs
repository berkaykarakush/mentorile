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

    public async Task<List<DiscountViewModel>> GetAllDiscountAsync()
    {
        var response = await _httpClient.GetAsync("discounts/get-all");
        if(!response.IsSuccessStatusCode) return new List<DiscountViewModel>();

        var result = await response.Content.ReadFromJsonAsync<Result<List<DiscountViewModel>>>();
        return result.Data;
    }

    public async Task<List<DiscountViewModel>> GetAllDiscountByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"discounts/user-get-all/{userId}");
        if(!response.IsSuccessStatusCode) return new List<DiscountViewModel>();

        var result = await response.Content.ReadFromJsonAsync<Result<List<DiscountViewModel>>>();
        return result.Data;
    }

    public async Task<DiscountViewModel> GetDiscountAsync(string code)
    {
        var response = await _httpClient.GetAsync($"discounts/{code}/{_sharedIdentityService.GetUserId}");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<DiscountViewModel>>();
        return result.Data;
    }

    public async Task<DiscountViewModel> GetDiscountByIdAsync(string discountId)
    {
        var response = await _httpClient.GetAsync($"discounts/get-by-id/{discountId}");
        if(!response.IsSuccessStatusCode) return new DiscountViewModel();

        var result = await response.Content.ReadFromJsonAsync<Result<DiscountViewModel>>();
        return result.Data;
    }
   public async Task<bool> CreateDiscountAsnyc(CreateDiscountInput createDiscountInput)
    {
        var response = await _httpClient.PostAsJsonAsync<CreateDiscountInput>("discounts/create", createDiscountInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateDiscountAsnyc(UpdateDiscountInput updateDiscountInput)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateDiscountInput>("discounts", updateDiscountInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteDiscountAsnyc(string discountId)
    {
        var respone = await _httpClient.DeleteAsync($"discounts/{discountId}");
        return respone.IsSuccessStatusCode;
    }
}