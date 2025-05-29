using System.Net;
using System.Security.Claims;
using MassTransit;
using Mentorile.Shared.Messages.Queries;
using Mentorile.Shared.Messages.Responses;

namespace Mentorile.Gateway.API.Handlers;
public class AccessControlDelegatingHandler : DelegatingHandler
{
    private readonly IRequestClient<UserAccessCheckQuery> _requestClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessControlDelegatingHandler(
        IRequestClient<UserAccessCheckQuery> requestClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _requestClient = requestClient;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || !httpContext.User.Identity?.IsAuthenticated == true)
            return await base.SendAsync(request, cancellationToken);

        var userId = httpContext.User.FindFirst("sub")?.Value
            ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return new HttpResponseMessage(HttpStatusCode.Forbidden);

        var response = await _requestClient.GetResponse<UserAccessCheckResponse>(new UserAccessCheckQuery
        {
            UserId = userId,
            Target = "email_confirmed"
        });

        if (!response.Message.IsAccessGranted)
        {
            return new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent("Email confirmation required.")
            };
        }

        return await base.SendAsync(request, cancellationToken);
    }
}