using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Queries;
public class GetDiscountByIdQuery : IRequest<Result<DiscountDTO>>
{
    public string DiscountId { get; set; } = string.Empty;
}