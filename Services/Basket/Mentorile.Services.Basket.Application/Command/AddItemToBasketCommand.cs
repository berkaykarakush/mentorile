using MediatR;
using Mentorile.Services.Basket.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.Command;

public class AddItemToBasketCommand : IRequest<Result<bool>>
{
    // public BasketItemDTO? Item { get; set; }
    public string? ItemId { get; set; }
    public string? ItemName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? PhotoUri { get; set; }
    public string? Type { get; set; }
    public decimal TotalPrice => Price * Quantity; // Toplam fiyat, miktar ile çarpılacak
}