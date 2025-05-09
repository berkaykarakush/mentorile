using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Commands;
public class UpdatePaymentCommand : IRequest<Result<bool>>
{
    public string Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}