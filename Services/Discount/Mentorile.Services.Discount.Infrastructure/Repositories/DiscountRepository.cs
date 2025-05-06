using Mentorile.Services.Discount.Domain.Exceptions;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Services.Discount.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Discount.Infrastructure.Repositories;
public class DiscountRepository : IDiscountRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;

    public DiscountRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public async Task<Result<decimal>> ApplyDiscountAsync(string code, decimal totalPrice)
        => await _executor.ExecuteAsync(async () =>{
            var discount = await _appDbContext.Discounts
                .Where(d => d.Code == code && d.IsActive && d.ExpirationDate >= DateTime.UtcNow)
                .FirstOrDefaultAsync();
            if(discount == null) throw new DiscountNotFoundException();
            // take discount rate
            var discountAmount = discount.Rate / 100 * totalPrice;
            var discountedPrice = totalPrice - discountAmount;
            return discountedPrice;
        });

    public async Task<Result<bool>> CancelDiscountAsync(string code)
        => await _executor.ExecuteAsync(async () =>{
            var discount = await _appDbContext.Discounts
                .Where(d => d.Code == code && d.IsActive && d.ExpirationDate >= DateTime.UtcNow)
                .FirstOrDefaultAsync();
            if(discount == null) throw new DiscountNotFoundException();

            discount.IsActive = false;
            _appDbContext.Update(discount);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new DiscountCancelException();
            return result;
        });

    public async Task<Result<Domain.Entities.Discount>> CreateAsync(Domain.Entities.Discount discount)
        => await _executor.ExecuteAsync(async () =>{
            await _appDbContext.Discounts.AddAsync(discount);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new DiscountCreateException();
            return discount;
        });

    public async Task<Result<bool>> DeleteAsync(string discountId)
        => await _executor.ExecuteAsync(async () =>{
            var discount = await _appDbContext.Discounts.FindAsync(discountId);
            if(discount == null) throw new DiscountNotFoundException();

            discount.Delete();
            _appDbContext.Discounts.Update(discount);
            
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new DiscountDeleteException();
            return result;
        });
    
    public async Task<Result<PagedResult<Domain.Entities.Discount>>> GetAllDiscountAsync(PagingParams pagingParams)
        => await _executor.ExecuteAsync(async () =>{
            var query = _appDbContext.Discounts.Include(q => q.DiscountUsers);
            var totalCount = await query.CountAsync();
            var discounts = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();
            var paged = PagedResult<Domain.Entities.Discount>.Create(discounts, totalCount, pagingParams);
            return paged;
        });

    public async Task<Result<PagedResult<Domain.Entities.Discount>>> GetAllDiscountByUserIdAsync(string userId, PagingParams pagingParams)
        => await _executor.ExecuteAsync(async () =>{
            var query = _appDbContext.Discounts.Where(q => q.DiscountUsers.Any(q => q.UserId == userId));
            var totalCount = await query.CountAsync();
            var discounts = await query
                .Skip((pagingParams.PageNumber -1 ) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();
            var paged = PagedResult<Domain.Entities.Discount>.Create(discounts, totalCount, pagingParams);
            return paged;
        });

    public async Task<Result<Domain.Entities.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
        => await _executor.ExecuteAsync(async () =>{
            var discount = await _appDbContext.Discounts.Where(d => 
                d.Code == code && d.DiscountUsers.Any(q => q.UserId == userId) &&
                d.IsActive && d.ExpirationDate >= DateTime.UtcNow)
            .FirstOrDefaultAsync();
            if(discount == null) throw new DiscountNotFoundException();
            return discount;
        });

    public async Task<Result<Domain.Entities.Discount>> GetDiscountByIdAsync(string discountId)
        => await _executor.ExecuteAsync(async () =>{
            var discount = await _appDbContext.Discounts.FindAsync(discountId);
            if(discount == null) throw new DiscountNotFoundException();
            return discount;
        });

    public async Task<Result<bool>> UpdateAsync(Domain.Entities.Discount discount)
        => await _executor.ExecuteAsync(async () =>{
            _appDbContext.Discounts.Update(discount);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new DiscountUpdateException();
            return result;
        });
}