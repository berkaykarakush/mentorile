using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Commands;
public class UpdateDiscountCommand: IRequest<Result<bool>>
{
    public DiscountDTO Discount { get; set; }
}