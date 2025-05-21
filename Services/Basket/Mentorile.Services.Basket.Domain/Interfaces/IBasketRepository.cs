using Mentorile.Services.Basket.Domain.Entities;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Domain.Interfaces;
public interface IBasketRepository
{
    Task<Result<bool>> AddItemToBasketAsync(BasketItem item);
    Task<Result<bool>> RemoveItemFromBasketAsync(string itemId);
    Task<Result<bool>> ClearBasketAsync();
    Task<Result<decimal>> GetTotalAmountAsync();
    Task<Result<bool>> ApplyDiscountAsync(string discountCode);
    Task<Result<bool>> CancelDiscountAsync(string discountCode);
    Task<Result<Entities.Basket>> GetBasketAsync();
}