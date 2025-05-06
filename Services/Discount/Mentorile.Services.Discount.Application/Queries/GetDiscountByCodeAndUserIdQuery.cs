using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Queries;
public class GetDiscountByCodeAndUserIdQuery : IRequest<Result<DiscountDTO>>
{
    public string Code { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}