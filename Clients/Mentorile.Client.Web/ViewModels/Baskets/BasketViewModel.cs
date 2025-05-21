namespace Mentorile.Client.Web.ViewModels.Baskets;
public class BasketViewModel
{
    public string Id { get; set; }
    public string BuyerId { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new();
    public decimal DiscountPercentage { get; set; }
    public string DiscountCode { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool HasDiscount => DiscountPercentage > 0;
}
