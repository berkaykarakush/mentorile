using Mentorile.Client.Web.ViewModels.Discounts;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IDiscountService
{
    Task<List<DiscountViewModel>> GetAllDiscountAsync();
    Task<List<DiscountViewModel>> GetAllDiscountByUserIdAsync(string userId);
    Task<DiscountViewModel> GetDiscountAsync(string code);
    Task<DiscountViewModel> GetDiscountByIdAsync(string discountId);
    Task<bool> CreateDiscountAsnyc(CreateDiscountInput createDiscountCodeInput);
    Task<bool> UpdateDiscountAsnyc(UpdateDiscountInput updateDiscountCodeInput);
    Task<bool> DeleteDiscountAsnyc(string discountCodeId);
}