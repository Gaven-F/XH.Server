namespace XH_Server.Web.Middlewares;

public class GUtilsMiddleware(RequestDelegate next)
{
	public async Task Invoke(HttpContext context)
	{
		var fg = Console.ForegroundColor;

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
		Console.ForegroundColor = fg;

		try
		{
			await next(context);
		}
		catch (Exception e)
		{
			context.Response.ContentType = "application/text";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsync(string.Format("""
			ERROR!
			{0}
			{1}
			""", e.Message, e.Source));
		}

	}
}
