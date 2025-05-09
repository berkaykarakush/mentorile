using Mentorile.Services.Payment.Domain.Enums;

namespace Mentorile.Services.Payment.Application.DTOs;
public class PaymentDTO
{
    public string PaymentId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PaymentStatus PaymentStatus { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsRefunded { get; set; }
    public DateTime? RefundedAt { get; set; }
    public string? CardMaskedNumber { get; set; }
}