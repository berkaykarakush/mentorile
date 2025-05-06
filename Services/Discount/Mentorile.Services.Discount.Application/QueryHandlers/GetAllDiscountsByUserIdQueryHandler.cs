using AutoMapper;
using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Services.Discount.Application.Queries;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.QueryHandlers;
public class GetAllDiscountsByUserIdQueryHandler : IRequestHandler<GetAllDiscountsByUserIdQuery, Result<PagedResult<DiscountDTO>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetAllDiscountsByUserIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<DiscountDTO>>> Handle(GetAllDiscountsByUserIdQuery request, CancellationToken cancellationToken)
    {
        if(request.PagingParams == null) request.PagingParams = new PagingParams();
        var discounts = await _discountRepository.GetAllDiscountByUserIdAsync(request.UserId, request.PagingParams);
        if(!discounts.IsSuccess) return Result<PagedResult<DiscountDTO>>.Failure("Discounts not found.");

        var dtos = _mapper.Map<List<DiscountDTO>>(discounts.Data.Data);

        var paged = PagedResult<DiscountDTO>.Create(dtos, discounts.Data.TotalCount, request.PagingParams);
        return Result<PagedResult<DiscountDTO>>.Success(paged);
    }
}