using MassTransit;
using Mentorile.Services.Order.Infrastructure.Contexts;
using Mentorile.Shared.Messages.Events;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Order.Application.Consumers;
public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
{
    private readonly OrderDbContext _orderDbContext;

    public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
    {
        var orderItems = await _orderDbContext.OrderItems.Where(x => x.ItemId == context.Message.CourseId).ToListAsync();
        orderItems.ForEach(x => {
            x.UpdateOrderItem(context.Message.UpdatedName, x.PhotoUri, x.Price);
        });
        await _orderDbContext.SaveChangesAsync();
    }
}