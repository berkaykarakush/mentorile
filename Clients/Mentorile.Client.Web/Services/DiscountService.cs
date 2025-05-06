using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Discounts;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;

namespace Mentorile.Client.Web.Services;
public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    private readonly ISharedIdentityService _sharedIdentityService;
    private const string controllersName = "discounts";
    public DiscountService(HttpClient httpClient, ISharedIdentityService sharedIdentityService)
    {
        _httpClient = httpClient;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<PagedResult<DiscountViewModel>> GetAllDiscountAsync()
    {
        var response = await _httpClient.GetAsync($"{controllersName}");
        if(!response.IsSuccessStatusCode) return new PagedResult<DiscountViewModel>();

        var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<DiscountViewModel>>>();
        return result.Data;
    }

    public async Task<PagedResult<DiscountViewModel>> GetAllDiscountByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/by-user/{userId}");
        if(!response.IsSuccessStatusCode) return new PagedResult<DiscountViewModel>();

        var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<DiscountViewModel>>>();
        return result.Data;
    }

    public async Task<DiscountViewModel> GetByCodeAndUserIdAsync(string code)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/{code}/for-user/{_sharedIdentityService.GetUserId}");
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<DiscountViewModel>>();
        return result.Data;
    }

    public async Task<DiscountViewModel> GetDiscountByIdAsync(string discountId)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/{discountId}");
        if(!response.IsSuccessStatusCode) return new DiscountViewModel();

        var result = await response.Content.ReadFromJsonAsync<Result<DiscountViewModel>>();
        return result.Data;
    }
   public async Task<bool> CreateDiscountAsnyc(CreateDiscountInput createDiscountInput)
    {
        var response = await _httpClient.PostAsJsonAsync<CreateDiscountInput>($"{controllersName}", createDiscountInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateDiscountAsnyc(UpdateDiscountInput updateDiscountInput)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateDiscountInput>($"{controllersName}/{updateDiscountInput.Id}", updateDiscountInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteDiscountAsnyc(string discountId)
    {
        var respone = await _httpClient.DeleteAsync($"{controllersName}/{discountId}");
        return respone.IsSuccessStatusCode;
    }
}