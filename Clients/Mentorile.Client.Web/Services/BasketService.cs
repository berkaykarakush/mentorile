using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Baskets;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services;
public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;
    private readonly ICourseService _courseService;
    private const string controllersName = "baskets";
    public BasketService(HttpClient httpClient, ICourseService courseService)
    {
        _httpClient = httpClient;
        _courseService = courseService;
    }

    public async Task<bool> AddItemToBasketAsync(AddBasketItemInput input)
    {
    
        var item = await _courseService.GetCourseByIdAsync(input.ItemId);
        input.ItemId = item.Id;
        input.ItemName = item.Name;
        input.PhotoUri = item.PhotoUri;
        input.Price = 10;
        input.Quantity = 1;
        input.Type = "Course";

        
        var response = await _httpClient.PostAsJsonAsync($"{controllersName}/add-item", input);

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

        var response = await _httpClient.PostAsJsonAsync($"{controllersName}/apply-discount/{discountCode}", new StringContent(""));
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

        var response = await _httpClient.PostAsJsonAsync($"{controllersName}/cancel-discount/{discountCode}", new StringContent(""));
        if(!response.IsSuccessStatusCode){
            var error = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(error);
        }
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ClearBasketAsync()
    {
        var response = await _httpClient.DeleteAsync($"{controllersName}/clear-basket");
        return response.IsSuccessStatusCode;
    }

    public async Task<BasketViewModel> GetBasketAsync()
    {
        // http://localhost:5000/services/basket/baskets
        var response = await _httpClient.GetAsync($"{controllersName}");
        if(!response.IsSuccessStatusCode) return null;
        
        var result = await response.Content.ReadFromJsonAsync<Result<BasketViewModel>>();
        return result.Data;
    }

    public async Task<bool> RemoveItemFromBasketAsync(string itemId)
    {
        var response = await _httpClient.DeleteAsync($"{controllersName}/delete-item/{itemId}");
        if(!response.IsSuccessStatusCode){
            var error = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine($"Hata; {error}");
        }
        return response.IsSuccessStatusCode;;
    }
}