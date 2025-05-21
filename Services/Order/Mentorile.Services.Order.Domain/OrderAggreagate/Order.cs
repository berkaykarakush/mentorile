using Mentorile.Services.Order.Domain.Core;

namespace Mentorile.Services.Order.Domain.OrderAggreagate
{
    public class Order : Entity, IAggregateRoot
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // Parametresiz constructor (EF Core için)
        public Order()
        {
            
        }

        // Parametreli constructor (Domain logic için)
        public Order(string orderId, string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.UtcNow;
            Id = orderId;
            BuyerId = buyerId;  // Doğru parametre ataması
            Address = address;  // Doğru parametre ataması
        }

        public void AddOrderItem(string itemId, string itemName, decimal price, string photoUri)
        {
            var existProduct = _orderItems.Any(x => x.ItemId == itemId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(itemId, itemName, photoUri, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
