using System.Text.Json;
using Mentorile.Services.Basket.DTOs;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;

namespace Mentorile.Services.Basket.Services;
public class BasketService : IBasketService
{
    private readonly IRedisService _redisService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketService(IRedisService redisService, ISharedIdentityService sharedIdentityService)
    {
        _redisService = redisService;
        _sharedIdentityService = sharedIdentityService;
    }

    private string GetBasketKey(string basketId) => $"basket:{basketId}";

    private async Task<(string basketKey, BasketDTO basket)> GetOrCreateBasketAsync()
    {
        var userId = _sharedIdentityService.GetUserId;
        var basketIdResult = await _redisService.GetBasketIdForUserAsync(userId);
        string basketId = basketIdResult.Data;

        if (string.IsNullOrEmpty(basketId))
        {
            basketId = Guid.NewGuid().ToString();
            await _redisService.SetStringAsync($"user:{userId}:basketId", basketId);
        }

        var basketKey = GetBasketKey(basketId);
        var redisResult = await _redisService.GetStringAsync(basketKey);

        BasketDTO basket;
        if (string.IsNullOrEmpty(redisResult.Data))
        {
            basket = new BasketDTO { Id = basketId };
        }
        else
        {
            basket = JsonSerializer.Deserialize<BasketDTO>(redisResult.Data) ?? new BasketDTO { Id = basketId };
        }

        return (basketKey, basket);
    }

    public async Task<Result<bool>> AddItemToBasketAsync(BasketItemDTO item)
    {
        var (key, basket) = await GetOrCreateBasketAsync();

        var existingItem = basket.Items.FirstOrDefault(x => x.ItemId == item.ItemId);
        if (existingItem != null)
            existingItem.Quantity += item.Quantity;
        else
            basket.Items.Add(item);

        // Sepetin toplamını yeniden hesaplıyoruz
        var json = JsonSerializer.Serialize(basket);
        var success = await _redisService.SetStringAsync(key, json, TimeSpan.FromDays(1));
        return success.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to add item.");
    }

    public async Task<Result<bool>> RemoveItemFromBasketAsync(string itemId)
    {
        var (key, basket) = await GetOrCreateBasketAsync();

        var itemToRemove = basket.Items.FirstOrDefault(x => x.ItemId == itemId);
        if (itemToRemove != null)
        {
            basket.Items.Remove(itemToRemove);
        }

        // Sepetin toplamını yeniden hesaplıyoruz
        var json = JsonSerializer.Serialize(basket);
        var success = await _redisService.SetStringAsync(key, json, TimeSpan.FromDays(1));
        return success.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to remove item.");
    }

    public async Task<Result<bool>> ClearBasketAsync()
    {
        var userId = _sharedIdentityService.GetUserId;
        var basketIdResult = await _redisService.GetBasketIdForUserAsync(userId);

        if (!basketIdResult.IsSuccess || string.IsNullOrEmpty(basketIdResult.Data))
            return Result<bool>.Failure("Basket not found.");

        var key = GetBasketKey(basketIdResult.Data);
        var deleted = await _redisService.RemoveKeyAsync(key);
        return deleted.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to clear basket.");
    }

    public async Task<Result<decimal>> GetTotalAmountAsync()
    {
        var (_, basket) = await GetOrCreateBasketAsync();
        return Result<decimal>.Success(basket.TotalAmount);
    }

   public async Task<Result<bool>> ApplyDiscountAsync(string discountCode)
    {
        var (key, basket) = await GetOrCreateBasketAsync();

        if (string.IsNullOrWhiteSpace(discountCode))
            return Result<bool>.Failure("Kupon kodu geçersiz.");

        // Örnek sabit kuponlar (ileride bu DiscountAPI'den çekilebilir)
        var availableCoupons = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            { "DISCOUNT10", 10 },
            { "DISCOUNT20", 20 },
            { "SPRING25", 25 }
        };

        if (!availableCoupons.TryGetValue(discountCode, out var discountPercentage))
            return Result<bool>.Failure("Kupon kodu bulunamadı veya geçersiz.");

        basket.DiscountCode = discountCode;
        basket.DiscountPercentage = discountPercentage;

        var json = JsonSerializer.Serialize(basket);
        var success = await _redisService.SetStringAsync(key, json, TimeSpan.FromDays(1));
        
        return success.IsSuccess 
            ? Result<bool>.Success(true) 
            : Result<bool>.Failure("İndirim uygulanırken bir hata oluştu.");
    }

    public async Task<Result<BasketDTO>> GetBasketAsync()
    {
        var (_, basket) = await GetOrCreateBasketAsync();
        return Result<BasketDTO>.Success(basket);
    }

    public async Task<Result<bool>> CancelDiscountAsync(string discountCode)
    {
        var (key, basket) = await GetOrCreateBasketAsync();
        if(string.IsNullOrEmpty(basket.DiscountCode) || !string.Equals(basket.DiscountCode, discountCode, StringComparison.OrdinalIgnoreCase)) return Result<bool>.Failure("İndirim kodu bulunamadı veya eşleşmiyor.");
        basket.DiscountCode = null;
        basket.DiscountPercentage = 0;

        var json = JsonSerializer.Serialize(basket);
        var success = await _redisService.SetStringAsync(key, json, TimeSpan.FromDays(1));
        return success.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure("İndirim kaldırılırken bir hata oluştu.");
    }
}