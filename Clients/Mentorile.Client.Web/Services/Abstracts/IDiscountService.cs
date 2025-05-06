using Mentorile.Client.Web.ViewModels.Discounts;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IDiscountService
{
    Task<PagedResult<DiscountViewModel>> GetAllDiscountAsync();
    Task<PagedResult<DiscountViewModel>> GetAllDiscountByUserIdAsync(string userId);
    Task<DiscountViewModel> GetByCodeAndUserIdAsync(string code);
    Task<DiscountViewModel> GetDiscountByIdAsync(string discountId);
    Task<bool> CreateDiscountAsnyc(CreateDiscountInput createDiscountCodeInput);
    Task<bool> UpdateDiscountAsnyc(UpdateDiscountInput updateDiscountCodeInput);
    Task<bool> DeleteDiscountAsnyc(string discountCodeId);
}