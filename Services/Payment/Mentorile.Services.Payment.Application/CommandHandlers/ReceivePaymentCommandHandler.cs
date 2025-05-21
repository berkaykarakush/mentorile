using MassTransit;
using MediatR;
using Mentorile.Services.Payment.Application.Commands;
using Mentorile.Services.Payment.Domain.Interfaces;
using Mentorile.Shared.Common;
using Mentorile.Shared.Messages.Commands;

namespace Mentorile.Services.Payment.Application.CommandHandlers;
public class ReceivePaymentCommandHandler : IRequestHandler<ReceivePaymentCommand, Result<string>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public ReceivePaymentCommandHandler(IPaymentRepository paymentRepository, ISendEndpointProvider sendEndpointProvider)
    {
        _paymentRepository = paymentRepository;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<Result<string>> Handle(ReceivePaymentCommand request, CancellationToken cancellationToken)
    {
        var paymentResult = await _paymentRepository.GetByIdAsync(request.PaymentId);
        if(!paymentResult.IsSuccess) return Result<string>.Failure("Payment not found.");

        var payment = paymentResult.Data;
        payment.OrderId = request.Order.OrderId;
        payment.TransactionId = request.TransactionId;
        payment.GatewayResponse = request.GatewayResponse;
        payment.CardMaskedNumber = request.CardMaskedNumber;
        payment.CardType = request.CardType;
        payment.ClientIpAddress = request.ClientIpAddress;
        payment.PaymentStatus = Domain.Enums.PaymentStatus.Completed;
        payment.CompletedAt = DateTime.UtcNow;
        var result = await _paymentRepository.UpdateAsync(payment);

        if(result.IsSuccess)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));
            var createOrderMessageCommand = new CreateOrderMessageCommand();
            createOrderMessageCommand.BuyerId = request.Order.BuyerId;
            createOrderMessageCommand.OrderId = request.Order.OrderId;
            
            createOrderMessageCommand.Address = new Address()
            {
                Province = request.Order.Address.Province,
                District = request.Order.Address.District,
                Street = request.Order.Address.Street,
                ZipCode = request.Order.Address.ZipCode,
                Line = request.Order.Address.Line
            };

            createOrderMessageCommand.OrderItems = request.Order.OrderItems.Select(x => new OrderItem{
                ItemId = x.ItemId,
                ItemName = x.ItemName,
                PhotoUri = x.PhotoUri,
                Price = x.Price
            }).ToList();
            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
            return Result<string>.Success(createOrderMessageCommand.OrderId, "Payment received and updated successfully.");
        }
        return Result<string>.Failure("Failed to payment received.");
    }
}