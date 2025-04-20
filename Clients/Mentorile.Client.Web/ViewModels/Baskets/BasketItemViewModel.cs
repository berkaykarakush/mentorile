namespace Mentorile.Client.Web.ViewModels.Baskets;
public class BasketItemViewModel
{
    public int Quantity { get; set; } = 1;

    public string ItemId { get; set; }  // Tipine göre kursId / membershipId vs olabilir
    public string ItemName { get; set; }
    public string Type { get; set; } // course, membership, bundle, etc.

    public string? PictureUri { get; set; }

    public decimal Price { get; set; }

    private decimal? DiscountAppliedPrice;

    public decimal GetCurrentPrice => DiscountAppliedPrice ?? Price;

    public void AppliedDiscount(decimal discountPrice)
    {
        DiscountAppliedPrice = discountPrice;
    }
}  