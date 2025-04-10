using Mentorile.Services.Discount.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Services;
public interface IDiscountService
{
    Task<Result<List<DiscountDTO>>> GetAllAsync();
    Task<Result<DiscountDTO>> GetByCodeAndUserIdAsync(string code, string userId);
    Task<Result<DiscountDTO>> CreateAsync(CreateDiscountDTO createDiscountDTO);
    Task<Result<bool>> DeleteAsync(string discountId);
}