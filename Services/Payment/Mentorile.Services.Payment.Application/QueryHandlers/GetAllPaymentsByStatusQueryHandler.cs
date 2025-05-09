using AutoMapper;
using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Services.Payment.Application.Queries;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.QueryHandlers;
public class GetAllPaymentsByStatusQueryHandler : IRequestHandler<GetAllPaymentsByStatusQuery, Result<PagedResult<PaymentDTO>>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetAllPaymentsByStatusQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<PaymentDTO>>> Handle(GetAllPaymentsByStatusQuery request, CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetAllByPaymentStatusAsync(request.PaymentStatus, request.PagingParams);
        if(payments == null) return Result<PagedResult<PaymentDTO>>.Failure("Payments not found.");

        var dtos = _mapper.Map<List<PaymentDTO>>(payments.Data.Data);
        var paged = PagedResult<PaymentDTO>.Create(dtos, payments.Data.TotalCount, request.PagingParams);
        return Result<PagedResult<PaymentDTO>>.Success(paged);
    }
}