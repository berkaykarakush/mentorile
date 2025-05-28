namespace Mentorile.Shared.Messages.Events;

public class OrderCreatedEvent
{
    public string OrderId { get; set; }
    public string BuyerId { get; set; }
    public DateTime CreateAt { get; set; }
}