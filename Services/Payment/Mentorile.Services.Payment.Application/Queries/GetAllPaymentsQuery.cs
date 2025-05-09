using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Queries;
public class GetAllPaymentsQuery : IRequest<Result<PagedResult<PaymentDTO>>>
{
    public PagingParams PagingParams { get; set; } = new();
}