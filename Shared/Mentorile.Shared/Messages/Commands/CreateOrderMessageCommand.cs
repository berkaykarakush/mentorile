namespace Mentorile.Shared.Messages.Commands;
public class CreateOrderMessageCommand
{
    public string BuyerId { get; set; }
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public List<OrderItem> OrderItems { get; set; } = new();
    public Address Address { get; set; } = new();
}

public class OrderItem
{
    public string ItemId { get; set; }    
    public string ItemName { get; set; }
    public string PhotoUri { get; set; }
    public decimal Price { get; set; }
}

public class Address
{
    public string Province { get; set; }    
    public string District { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string Line { get; set; }
}