using Mentorile.Services.Discount.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Services;
public interface IDiscountService
{
    Task<Result<List<DiscountDTO>>> GetAllDiscountAsync();
    Task<Result<DiscountDTO>> GetByCodeAndUserIdAsync(string code, string userId);
    Task<Result<List<DiscountDTO>>> GetAllDiscountByUserIdAsync(string userId);
    Task<Result<DiscountDTO>> GetDiscountByIdAsync(string discountId);
    Task<Result<DiscountDTO>> CreateAsync(CreateDiscountDTO createDiscountDTO);
    Task<Result<DiscountDTO>> UpdateAsync(UpdateDiscountDTO updateDiscountDTO);
    Task<Result<bool>> DeleteAsync(string discountId);
    Task<Result<decimal>> ApplyDiscountAsync(string code, decimal totalPrice);
    Task<Result<bool>> CancelDiscountAsync(string code);
}