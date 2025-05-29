using Mentorile.Client.Web.Exceptions;

namespace Mentorile.Client.Web.Middlewares;
public class ExceptionRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnAuthorizeException)
        {
            context.Response.Redirect("/Auth/SignIn");
        }
        catch (EmailNotConfirmedException)
        {
            context.Response.Redirect("/Auth/ConfirmEmail");
        }
    }    
}