using AutoMapper;
using MediatR;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Services.Discount.Application.Queries;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.QueryHandlers;
public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, Result<DiscountDTO>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<DiscountDTO>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetDiscountByIdAsync(request.DiscountId);
        if(!discount.IsSuccess) return Result<DiscountDTO>.Failure("Discount not found.");

        var dto = _mapper.Map<DiscountDTO>(discount.Data);

        return Result<DiscountDTO>.Success(dto);
    }
}