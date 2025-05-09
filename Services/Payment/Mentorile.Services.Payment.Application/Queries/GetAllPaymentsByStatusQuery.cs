using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Services.Payment.Domain.Enums;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Queries;
public class GetAllPaymentsByStatusQuery : IRequest<Result<PagedResult<PaymentDTO>>>
{
    public PaymentStatus PaymentStatus { get; set; }    
    public PagingParams PagingParams { get; set; } = new();
}