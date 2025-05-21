using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.Command;
public class ApplyDiscountCommand : IRequest<Result<bool>>
{
    public string DiscountCode { get; set; }
}