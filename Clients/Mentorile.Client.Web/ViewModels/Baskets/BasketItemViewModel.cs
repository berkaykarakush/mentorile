namespace Mentorile.Client.Web.ViewModels.Baskets;

public class BasketItemViewModel
{
    public string? ItemId { get; set; }  // Tipine göre kursId / membershipId vs olabilir
    public string? ItemName { get; set; }
    public int Quantity { get; set; } = 1;
    public string? Type { get; set; } // course, membership, bundle, etc.
    public string? PhotoUri { get; set; }
    public decimal Price { get; set; }
    private decimal? DiscountAppliedPrice;
    public void AppliedDiscount(decimal discountPrice) => DiscountAppliedPrice = discountPrice;
    public decimal TotalPrice => Price * Quantity; // Toplam fiyat, miktar ile çarpılacak
}  