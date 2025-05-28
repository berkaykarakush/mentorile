using MassTransit;
using Mentorile.Services.Order.Infrastructure.Contexts;
using Mentorile.Shared.Messages.Commands;
using Mentorile.Shared.Messages.Events;

namespace Mentorile.Services.Order.Application.Consumers;
public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private readonly OrderDbContext _orderDbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext, IPublishEndpoint publishEndpoint)
    {
        _orderDbContext = orderDbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var newAddress = new Domain.OrderAggreagate.Address(
            context.Message.Address.Province,
            context.Message.Address.District,
            context.Message.Address.Street,
            context.Message.Address.Line,
            context.Message.Address.ZipCode
        );

        var order = new Domain.OrderAggreagate.Order(context.Message.OrderId, context.Message.BuyerId, newAddress);
        context.Message.OrderItems.ForEach(x =>
        {
            order.AddOrderItem(x.ItemId, x.ItemName, x.Price, x.PhotoUri);
        });

        await _orderDbContext.Orders.AddAsync(order);
        await _orderDbContext.SaveChangesAsync();
        await _publishEndpoint.Publish<OrderCreatedEvent>(new OrderCreatedEvent
        {
            OrderId = order.Id,
            BuyerId = context.Message.BuyerId
        });
    }
}