using MassTransit;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Messages.Queries;
using Mentorile.Shared.Messages.Responses;

namespace Mentorile.IdentityServer.Consumers;

public class UserAccessCheckQueryConsumer : IConsumer<UserAccessCheckQuery>
{
    private readonly IUserAccessService _userAccessService;

    public UserAccessCheckQueryConsumer(IUserAccessService userAccessService)
    {
        _userAccessService = userAccessService;
    }

    public async Task Consume(ConsumeContext<UserAccessCheckQuery> context)
    {
        var result = await _userAccessService.CheckAccessAsync(context.Message.UserId, context.Message.Target);

        await context.RespondAsync(new UserAccessCheckResponse
        {
            IsAccessGranted = result.isGranted,
            Reason = result.reason
        });
    }
}