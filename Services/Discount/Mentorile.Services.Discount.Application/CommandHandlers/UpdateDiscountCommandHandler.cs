using AutoMapper;
using MediatR;
using Mentorile.Services.Discount.Application.Commands;
using Mentorile.Services.Discount.Application.DTOs;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.CommandHandlers;
public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Result<bool>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = _mapper.Map<Domain.Entities.Discount>(request.Discount);
        if(discount == null) return Result<bool>.Failure("Mapping failed.");

        var result = await _discountRepository.UpdateAsync(discount);
        if(!result.IsSuccess) return Result<bool>.Failure("Failed to discount updated."); 

        return Result<bool>.Success(result.IsSuccess, "Discount updated successfully.");
    }
}