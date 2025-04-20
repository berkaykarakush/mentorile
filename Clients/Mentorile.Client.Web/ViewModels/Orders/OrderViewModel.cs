namespace Mentorile.Client.Web.ViewModels.Orders;
public class OrderViewModel
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public AddressViewModel Address { get; set; }
    public string BuyerId { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; }
}