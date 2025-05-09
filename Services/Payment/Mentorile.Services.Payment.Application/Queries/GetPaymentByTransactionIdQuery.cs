using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Queries;
public class GetPaymentByTransactionIdQuery : IRequest<Result<PaymentDTO>>
{
    public string TransactionId { get; set; } = string.Empty;    
}