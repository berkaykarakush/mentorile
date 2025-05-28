using Duende.IdentityServer.Extensions;
using Mentorile.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentorile.IdentityServer.Middlewares;

public class EmailConfirmationMiddleware
{
    private readonly RequestDelegate _next;

    public EmailConfirmationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            await _next(context);
            return;
        }

        var userId = context.User.FindFirst("sub")?.Value;
        if (userId == null)
        {
            await _next(context);
            return;
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            await _next(context);
            return;
        }

        var path = context.Request.Path.Value?.ToLower();

        // Email onaylı değilse ve ConfirmEmail sayfası dışında bir yere gitmeye çalışıyorsa yönlendir
        if (!user.EmailConfirmed && !path.StartsWith("/auth/confimEmail"))
        {
            context.Response.Redirect("/auth/confirmEmail");
            return;
        }
        await _next(context);
    }
}