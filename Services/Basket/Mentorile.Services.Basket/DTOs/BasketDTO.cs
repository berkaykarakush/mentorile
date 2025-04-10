namespace Mentorile.Services.Basket.DTOs;
public class BasketDTO
{
    public string Id { get; set; }  // Sepet kimliği
    public string BuyerId { get; set; }  // Kullanıcı kimliği (BuyerId)
    public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
    public decimal DiscountAmount { get; set; }  // İndirim miktarı
    public decimal FinalAmount => TotalAmount - DiscountAmount;  // İndirimli toplam
    public string CouponCode { get; set; }  // Uygulanan kupon kodu

    // Sepetin toplam tutarını hesaplamak için yardımcı metod
    public decimal TotalAmount => CalculateTotalAmount();

    private decimal CalculateTotalAmount()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total += item.TotalPrice;
        }
        return total;
    }
}