using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Baskets;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services;
public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;
    private readonly ICourseService _courseService;
    private readonly IDiscountService _discountService;

    public BasketService(HttpClient httpClient, ICourseService courseService, IDiscountService discountService)
    {
        _httpClient = httpClient;
        _courseService = courseService;
        _discountService = discountService;
    }

    public async Task<bool> AddItemToBasketAsync(BasketItemViewModel basketItemViewModel)
    {
    
        var item = await _courseService.GetCourseByIdAsync(basketItemViewModel.ItemId);
        basketItemViewModel.ItemId = item.Id;
        basketItemViewModel.ItemName = item.Name;
        basketItemViewModel.Price = 10;
        basketItemViewModel.Quantity = 1;
        basketItemViewModel.Type = "Course";

        var response = await _httpClient.PostAsJsonAsync("baskets/add-item", basketItemViewModel);

        if (!response.IsSuccessStatusCode) 
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error); 
        }
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ApplyDiscountAsync(string discountCode)
    {
        var basket = await GetBasketAsync();
        if (basket == null || string.IsNullOrEmpty(discountCode)) return false;

        var response = await _httpClient.PostAsJsonAsync($"baskets/apply-discount/{discountCode}", new StringContent(""));
        if(!response.IsSuccessStatusCode){
            var error = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(error);
        }
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CancelDiscountAsync(string discountCode)
    {
        var basket = await GetBasketAsync();
        if(basket == null || string.IsNullOrEmpty(discountCode)) return false;

        var response = await _httpClient.PostAsJsonAsync($"baskets/cancel-discount/{discountCode}", new StringContent(""));
        if(!response.IsSuccessStatusCode){
            var error = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(error);
        }
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ClearBasketAsync()
    {
        var response = await _httpClient.DeleteAsync("baskets/clear-basket");
        return response.IsSuccessStatusCode;
    }

    public async Task<BasketViewModel> GetBasketAsync()
    {
        // http://localhost:5000/services/basket/baskets
        var response = await _httpClient.GetAsync("baskets/get-basket");
        if(!response.IsSuccessStatusCode) return null;
        
        var result = await response.Content.ReadFromJsonAsync<Result<BasketViewModel>>();
        return result.Data;
    }

    public async Task<bool> RemoveItemFromBasketAsync(string itemId)
    {
        var response = await _httpClient.DeleteAsync($"baskets/delete-item/{itemId}");
        if(!response.IsSuccessStatusCode){
            var error = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine($"Hata; {error}");
        }
        return response.IsSuccessStatusCode;;
    }
}