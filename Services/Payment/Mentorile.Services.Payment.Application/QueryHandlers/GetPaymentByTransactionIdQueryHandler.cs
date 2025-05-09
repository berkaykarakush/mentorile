using AutoMapper;
using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Services.Payment.Application.Queries;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.QueryHandlers;
public class GetPaymentByTransactionIdQueryHandler : IRequestHandler<GetPaymentByTransactionIdQuery, Result<PaymentDTO>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentByTransactionIdQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaymentDTO>> Handle(GetPaymentByTransactionIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByTransactionIdAsync(request.TransactionId);
        if(payment == null) return Result<PaymentDTO>.Failure("Payment not found.");

        var dto = _mapper.Map<PaymentDTO>(payment);
        return Result<PaymentDTO>.Success(dto);
    }
}