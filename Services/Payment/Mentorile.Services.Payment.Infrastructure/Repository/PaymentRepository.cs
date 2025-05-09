using Mentorile.Services.Payment.Domain.Enums;
using Mentorile.Services.Payment.Domain.Exceptions;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Services.Payment.Infrastrucutre.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Payment.Infrastrucutre.Repository;
public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;

    public PaymentRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public async Task<Result<string>> CreateAsync(Domain.Entities.Payment payment)
        => await _executor.ExecuteAsync(async () =>
        {
            await _appDbContext.Payments.AddAsync(payment);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new Exception("Failed to payment created."); 
            return payment.PaymentId;
        }); 

    public async Task<Result<PagedResult<Domain.Entities.Payment>>> GetAllAsync(PagingParams pagingParams)
    => await _executor.ExecuteAsync(async () =>
    {
        var query = _appDbContext.Payments;
        return await GetPagedResultAsync(query, pagingParams);
    });

    public async Task<Result<PagedResult<Domain.Entities.Payment>>> GetAllByPaymentStatusAsync(PaymentStatus status, PagingParams pagingParams)
    => await _executor.ExecuteAsync(async () =>
    {
        var query = _appDbContext.Payments.Where(x => x.PaymentStatus == status);
        return await GetPagedResultAsync(query, pagingParams);
    });

    public Task<Result<PagedResult<Domain.Entities.Payment>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams)
        => _executor.ExecuteAsync(async () =>
        {
            var query = _appDbContext.Payments.Where(x => x.UserId == userId);
            return await GetPagedResultAsync(query, pagingParams);
        });

    public async Task<Result<Domain.Entities.Payment>> GetByIdAsync(string paymentId)
        => await _executor.ExecuteAsync(async () =>
        {
            var payment = await _appDbContext.Payments.FindAsync(paymentId);
            if(payment == null) throw new PaymentNotFoundException();
            return payment;
        });
    public async Task<Result<Domain.Entities.Payment>> GetByOrderIdAsync(string orderId)
        => await _executor.ExecuteAsync(async () =>
        {
            var payment = await _appDbContext.Payments.FirstOrDefaultAsync(x => x.OrderId == orderId);
            if(payment == null) throw new PaymentNotFoundException();
            return payment;
        });

    public async Task<Result<Domain.Entities.Payment>> GetByTransactionIdAsync(string transactionId)
        => await _executor.ExecuteAsync(async () => 
        {
            var payment = await _appDbContext.Payments.FirstOrDefaultAsync(p => p.TransactionId == transactionId);
            if(payment == null) throw new PaymentNotFoundException();
            return payment;
        });

    public async Task<Result<bool>> UpdateAsync(Domain.Entities.Payment payment)
        => await _executor.ExecuteAsync(async () => 
        {
            _appDbContext.Update(payment);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new PaymentUpdateException();
            return result;
        });

    private async Task<PagedResult<Domain.Entities.Payment>> GetPagedResultAsync(IQueryable<Domain.Entities.Payment> query, PagingParams pagingParams)
    {
        var totalCount = await query.CountAsync();
         var payments = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();
        var paged = PagedResult<Domain.Entities.Payment>.Create(payments, totalCount, pagingParams);
        return paged;
    }
}