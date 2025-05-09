using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Queries;
public class GetPaymentByOrderIdQuery : IRequest<Result<PaymentDTO>>
{
    public string OrderId { get; set; } = string.Empty;
}