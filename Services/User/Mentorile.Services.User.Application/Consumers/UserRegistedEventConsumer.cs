using MassTransit;
using Mentorile.Services.User.Domain.Entities;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Messages.Events;

namespace Mentorile.Services.User.Application.Consumers;
public class UserRegistedEventConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IUserRepository _userRepository;

    public UserRegistedEventConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        try
        {
            var message = context.Message;
            var result = await _userRepository.AddAsync(new UserProfile
            {
                Id = message.UserId,
                Name = message.Name,
                Surname = message.Surname,
                Email = message.Email,
                PhoneNumber = message.PhoneNumber,
                CreateAt = message.CreateAt
            });

            if (!result.IsSuccess)
            {
                Console.WriteLine("DB write failed!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in consumer: {ex.Message}");
            throw; // re-throw so MassTransit can retry or fail properly
        }
    }
}