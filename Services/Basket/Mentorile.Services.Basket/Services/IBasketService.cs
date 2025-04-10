using Mentorile.Services.Basket.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Services;
public interface IBasketService
{
    Task<Result<bool>> AddItemToBasketAsync(BasketItemDTO item);
    Task<Result<bool>> RemoveItemFromBasketAsync(string itemId);
    Task<Result<bool>> ClearBasketAsync();
    Task<Result<decimal>> GetTotalAmountAsync();
    Task<Result<bool>> ApplyCouponAsync(string couponCode);
    Task<Result<BasketDTO>> GetBasketAsync();
}