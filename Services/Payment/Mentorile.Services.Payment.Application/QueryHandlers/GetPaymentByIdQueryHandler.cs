using AutoMapper;
using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Services.Payment.Application.Queries;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.QueryHandlers;
public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDTO>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaymentDTO>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.Id);
        if(payment == null) return Result<PaymentDTO>.Failure("Payment not found.");

        var dto = _mapper.Map<PaymentDTO>(payment.Data);
        return Result<PaymentDTO>.Success(dto);
    }
}