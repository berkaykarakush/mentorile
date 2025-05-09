using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Queries;
public class GetAllPaymentByUserIdQuery : IRequest<Result<PagedResult<PaymentDTO>>>
{
    public string UserId { get; set; }    
    public PagingParams PagingParams { get; set; } = new();
}