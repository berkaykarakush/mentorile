namespace Mentorile.Client.Web.ViewModels.Orders;
public class OrderCreateInput
{
    public string BuyerId { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public List<OrderItemCreateInput> OrderItems { get; set; } = new();
    public AddressCreateInput Address { get; set; }    
}