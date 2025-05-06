using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Domain.Interfaces;
public interface IDiscountRepository
{
    Task<Result<PagedResult<Entities.Discount>>> GetAllDiscountAsync(PagingParams pagingParams);
    Task<Result<Entities.Discount>> GetByCodeAndUserIdAsync(string code, string userId);
    Task<Result<PagedResult<Entities.Discount>>> GetAllDiscountByUserIdAsync(string userId, PagingParams pagingParams);
    Task<Result<Entities.Discount>> GetDiscountByIdAsync(string discountId);
    Task<Result<Entities.Discount>> CreateAsync(Entities.Discount discount);
    Task<Result<bool>> UpdateAsync(Entities.Discount discount);
    Task<Result<bool>> DeleteAsync(string discountId);
    Task<Result<decimal>> ApplyDiscountAsync(string code, decimal totalPrice);
    Task<Result<bool>> CancelDiscountAsync(string code);
}