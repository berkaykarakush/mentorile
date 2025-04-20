using Mentorile.Client.Web.ViewModels.Baskets;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IBasketService
{
    Task<bool> AddItemToBasketAsync(BasketItemViewModel basketItemViewModel);
    Task<bool> RemoveItemFromBasketAsync(string itemId);
    Task<BasketViewModel> GetBasketAsync();
    Task<bool> ApplyDiscountAsync(string discountCode);
    Task<bool> CancelDiscountAsync(string discountCode);
    Task<bool> ClearBasketAsync();
}