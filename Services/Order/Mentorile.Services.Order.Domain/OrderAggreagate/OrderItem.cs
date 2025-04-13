using Mentorile.Services.Order.Domain.Core;

namespace Mentorile.Services.Order.Domain.OrderAggreagate;
public class OrderItem : Entity
{
    public OrderItem(string itemId, string itemName, string pictureUri, decimal price)
    {
        ItemId = itemId;
        ItemName = itemName;
        PictureUri = pictureUri;
        Price = price;
    }

    public string ItemId { get; private set; }    
    public string ItemName { get; private set; }
    public string PictureUri { get; private set; }
    public Decimal Price { get; private set; }

    public void UpdateOrderItem(string itemName, string pcitureUri, decimal price){
        ItemName = itemName;
        PictureUri = pcitureUri;
        Price = price;
    }
}