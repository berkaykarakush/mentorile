using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Commands;
public class CancelDiscountCommand : IRequest<Result<bool>>
{
    public string Code { get; set; } = string.Empty;
}