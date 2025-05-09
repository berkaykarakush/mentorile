using Mentorile.Services.Payment.Domain.Entities;
using Mentorile.Services.Payment.Domain.Enums;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Domain.Interfaces;
public interface IPaymentRepository
{
    Task<Result<PagedResult<Entities.Payment>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<PagedResult<Entities.Payment>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams);
    Task<Result<PagedResult<Entities.Payment>>> GetAllByPaymentStatusAsync(PaymentStatus status, PagingParams pagingParams);
    Task<Result<Entities.Payment>> GetByIdAsync(string paymentId);
    Task<Result<Entities.Payment>> GetByOrderIdAsync(string orderId);
    Task<Result<Entities.Payment>> GetByTransactionIdAsync(string transactionId);
    Task<Result<string>> CreateAsync(Entities.Payment payment);
    Task<Result<bool>> UpdateAsync(Entities.Payment payment);
}