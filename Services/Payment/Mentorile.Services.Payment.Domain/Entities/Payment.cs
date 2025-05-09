using Mentorile.Services.Payment.Domain.Enums;

namespace Mentorile.Services.Payment.Domain.Entities;
public class Payment
{
    public string PaymentId { get; set; } = Guid.NewGuid().ToString();
    public string? OrderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = "TRY";
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditCard;
    public string? TransactionId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public string? CardMaskedNumber { get; set; }
    public string? CardType { get; set; }
    public string? FailureReason { get; set; }
    public bool IsRefunded { get; set; }
    public DateTime? RefundedAt { get; set; }
    public string? GatewayResponse { get; set; }
    public string? ClientIpAddress { get; set; }
    public string? CorrelationId { get; set; }
}