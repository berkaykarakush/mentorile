using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.Command;
public class CancelDiscountCommand : IRequest<Result<bool>>
{
    public string DiscountCode { get; set; }
}