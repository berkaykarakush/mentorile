namespace Mentorile.Client.Web.ViewModels.Discounts;
public class DiscountViewModel
{
    public string Id { get; set; } 
    public List<string> UserIds { get; set; } = new();
    public int Rate { get; set; }
    public string Code { get; set; }
    public bool IsActive { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedDate { get; set; }
}