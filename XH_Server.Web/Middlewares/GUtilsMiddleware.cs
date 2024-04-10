namespace XH_Server.Web.Middlewares;

public class GUtilsMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(string.Format("""
		─────────────
		REQUEST MESSAGE:	{0} || {1} || {2} || {3}
		─────────────

		""",
            DateTime.Now.ToLocalTime(),
            context.Request.HttpContext.GetRemoteIpAddressToIPv4() ?? context.Request.HttpContext.GetRemoteIpAddressToIPv6(),
            context.Request.Headers.Host,
            context.Request.Path.Value));

        Console.ResetColor();

        await next(context);
    }
}
