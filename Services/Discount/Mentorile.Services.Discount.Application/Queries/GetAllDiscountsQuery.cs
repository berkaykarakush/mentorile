using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Queries;
public class GetAllDiscountsQuery: IRequest<Result<PagedResult<DiscountDTO>>>
{
    public PagingParams? PagingParams { get; set; }
}