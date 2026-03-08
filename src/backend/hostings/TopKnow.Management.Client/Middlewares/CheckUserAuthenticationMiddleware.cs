namespace TopKnow.Management.Client.Middlewares;

public class CheckUserAuthenticationMiddleware
{
    private readonly RequestDelegate requestDelegate;

    public CheckUserAuthenticationMiddleware(RequestDelegate requestDelegate)
    {
        this.requestDelegate = requestDelegate;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.ToString().Contains("/Authentication"))
        {
            await requestDelegate(context);
            return;
        }

        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/Authentication/Login");
            return;
        }

        await requestDelegate(context);
    }
}
