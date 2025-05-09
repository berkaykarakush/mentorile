using AutoMapper;
using MediatR;
using Mentorile.Services.Payment.Application.Commands;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.CommandHandlers;
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<string>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = _mapper.Map<Domain.Entities.Payment>(request);
        var result = await _paymentRepository.CreateAsync(payment);
        if(!result.IsSuccess) return Result<string>.Failure("Failed to payment creating."); 
        return Result<string>.Success(result.Data);
    }
}