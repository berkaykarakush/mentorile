using AutoMapper;
using MediatR;
using Mentorile.Services.Payment.Application.Commands;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.CommandHandlers;
public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Result<bool>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.Id);
        if(payment == null) return Result<bool>.Failure("Payment not found.");

        // TODO: Gerekli guncelleme islemlerini gerceklestir.

        var result = await _paymentRepository.UpdateAsync(payment.Data);
        if(!result.IsSuccess) return Result<bool>.Failure("Failed to payment updating.");
        return Result<bool>.Success(result.IsSuccess, "Payment updated successfully.");
    }
}