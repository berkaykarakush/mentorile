using System.Text.Json;
using MassTransit;
using Mentorile.Services.Basket.Application.DTOs;
using Mentorile.Shared.Abstractions;
using Mentorile.Shared.Messages.Events;

namespace Mentorile.Services.Basket.Application.Consumers;
public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
{
    private readonly IRedisService _redisService;

    public CourseNameChangedEventConsumer(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
    {
      var keys = await _redisService.GetKeysAsync("basket:*"); // Tüm kullanıcı sepetlerini al

        foreach (var key in keys)
        {
            var redisResult = await _redisService.GetStringAsync(key);

            if (!redisResult.IsSuccess || string.IsNullOrEmpty(redisResult.Data))
                continue;

            var basket = JsonSerializer.Deserialize<Domain.Entities.Basket>(redisResult.Data);
            if (basket?.Items == null) continue;

            bool isUpdated = false;

            foreach (var item in basket.Items)
            {
                if (item.ItemId == context.Message.CourseId)
                {
                    item.ItemName = context.Message.UpdatedName;
                    isUpdated = true;
                }
            }

            if (isUpdated)
            {
                var updatedJson = JsonSerializer.Serialize(basket);
                await _redisService.SetStringAsync(key, updatedJson, TimeSpan.FromDays(1));

                Console.WriteLine($"[UPDATED] Course name updated for basket: {key}");
            }
        }
    }
}