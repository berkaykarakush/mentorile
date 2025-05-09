using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Commands;
public class CreatePaymentCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; }
}