namespace Mentorile.Services.Basket.DTOs;
public class BasketItemDTO
{
    public string ItemId { get; set; }
    public string ItemName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? PictureUri { get; set; }
    public string? Type { get; set; }
    public decimal TotalPrice => Price * Quantity; // Toplam fiyat, miktar ile çarpılacak
}