namespace Mentorile.Client.Web.ViewModels.Orders;
public class OrderCreatedViewModel
{
    public string OrderId { get; set; }
    public string Error { get; set; }
    public bool IsSuccessful { get; set; }
}