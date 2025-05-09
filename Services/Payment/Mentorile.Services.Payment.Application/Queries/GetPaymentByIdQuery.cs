using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Queries;
public class GetPaymentByIdQuery : IRequest<Result<PaymentDTO>>
{
    public string Id { get; set; } = string.Empty;
}