using Mentorile.Services.Basket.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Services;
public interface IBasketService
{
    Task<Result<bool>> AddItemToBasketAsync(BasketItemDTO item);
    Task<Result<bool>> RemoveItemFromBasketAsync(string itemId);
    Task<Result<bool>> ClearBasketAsync();
    Task<Result<decimal>> GetTotalAmountAsync();
    Task<Result<bool>> ApplyDiscountAsync(string discountCode);
    Task<Result<bool>> CancelDiscountAsync(string discountCode);
    Task<Result<BasketDTO>> GetBasketAsync();
}