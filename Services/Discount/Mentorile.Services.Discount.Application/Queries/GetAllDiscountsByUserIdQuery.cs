using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Queries;
public class GetAllDiscountsByUserIdQuery : IRequest<Result<PagedResult<DiscountDTO>>>
{
    public string UserId { get; set; } = string.Empty;
    public PagingParams? PagingParams { get; set; }
}