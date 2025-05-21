namespace Mentorile.Client.Web.ViewModels.Orders;
public class OrderItemCreateInput
{
    public string ItemId { get; set; }    
    public string ItemName { get; set; }
    public string PhotoUri { get; set; }
    public decimal Price { get; set; }
}