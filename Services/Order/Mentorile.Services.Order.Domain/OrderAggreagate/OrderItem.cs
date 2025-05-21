using Mentorile.Services.Order.Domain.Core;

namespace Mentorile.Services.Order.Domain.OrderAggreagate;
public class OrderItem : Entity
{
    public OrderItem(string itemId, string itemName, string photoUri, decimal price)
    {
        ItemId = itemId;
        ItemName = itemName;
        PhotoUri = photoUri;
        Price = price;
    }

    public string ItemId { get; private set; }    
    public string ItemName { get; private set; }
    public string PhotoUri { get; private set; }
    public Decimal Price { get; private set; }

    public void UpdateOrderItem(string itemName, string photoUri, decimal price){
        ItemName = itemName;
        PhotoUri = photoUri;
        Price = price;
    }
}