using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Commands;
public class ApplyDiscountCommand : IRequest<Result<decimal>>
{
    public string Code { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
}