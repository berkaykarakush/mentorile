using Mentorile.Client.Web.ViewModels.Discounts;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscountCodeAsync(string discountCode);
}