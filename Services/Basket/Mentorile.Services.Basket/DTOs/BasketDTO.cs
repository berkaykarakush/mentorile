namespace Mentorile.Services.Basket.DTOs;
public class BasketDTO
{
    public string Id { get; set; }  // Sepet kimliği
    public string BuyerId { get; set; }  // Kullanıcı kimliği
    public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
    public decimal DiscountPercentage { get; set; }  // İndirim yüzdesi (örneğin: 10)
    public string DiscountCode { get; set; }  // Uygulanan kupon kodu

    // Toplam tutar
    public decimal TotalAmount => CalculateTotalAmount();

    // İndirimli toplam tutar (final)
    public decimal FinalAmount => TotalAmount * (1 - DiscountPercentage / 100m);

    private decimal CalculateTotalAmount()
    {
        decimal total = 0;
        foreach (var item in Items) total += item.TotalPrice;
        return total;
    }
}