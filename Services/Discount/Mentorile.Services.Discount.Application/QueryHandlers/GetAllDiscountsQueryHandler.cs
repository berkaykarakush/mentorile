using AutoMapper;
using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Services.Discount.Application.Queries;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.QueryHandlers;
public class GetAllDiscountsQueryHandler : IRequestHandler<GetAllDiscountsQuery, Result<PagedResult<DiscountDTO>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetAllDiscountsQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<DiscountDTO>>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
    {
        if(request.PagingParams == null) request.PagingParams = new();
        var discounts = await _discountRepository.GetAllDiscountAsync(request.PagingParams);
        if(!discounts.IsSuccess) return Result<PagedResult<DiscountDTO>>.Failure("Discounts not found.");

        var dtos = _mapper.Map<List<DiscountDTO>>(discounts.Data.Data);

        var paged = PagedResult<DiscountDTO>.Create(dtos, discounts.Data.TotalCount, request.PagingParams);
        return Result<PagedResult<DiscountDTO>>.Success(paged);
    }
}