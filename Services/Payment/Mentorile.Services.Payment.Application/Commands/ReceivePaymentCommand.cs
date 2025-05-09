using MediatR;
using Mentorile.Services.Payment.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Payment.Application.Commands;
public class ReceivePaymentCommand : IRequest<Result<string>>
{
    public string? OrderId { get; set; }
    public string? UserId { get; set; }
    public string? PaymentId { get; set; }
    public string? TransactionId { get; set; }
    public string? Currency { get; set; }
    public string? GatewayResponse { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentStatus { get; set; }
    public string? CardMaskedNumber { get; set; }
    public string? CardType { get; set; }
    public string? ClientIpAddress { get; set; }
    public string? CardName { get; set; }    
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? CVV { get; set; }
    public decimal TotalPrice { get; set; }    
    public OrderDTO Order { get; set; } = new();
}