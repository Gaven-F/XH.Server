using System.Diagnostics;

namespace Server.Web.MiddleWares;

public class GUtilsMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var time = Stopwatch.StartNew();
        await next(context);
        time.Stop();

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(
            string.Format(
                $$""""
                {{"log:",-6}}Request Log
                {{string.Empty,6}}{{"Time:",-24}}{0}
                {{string.Empty,6}}{{"Origin:",-24}}{1}
                {{string.Empty,6}}{{"Request Path:",-24}}{{"{2}",-5}}{{(
                    context.Request.IsHttps ? "https://" : "http://"
                )}}{3}{4}
                {{string.Empty,6}}{{"Status Code:",-24}}{5}
                {{string.Empty,6}}{{"Content Type:",-24}}{6}
                {{string.Empty,6}}{{$"{time.ElapsedMilliseconds}ms",-24}}{7}bytes
                """",
                DateTime.Now.ToLocalTime(),
                context.Request.HttpContext.GetRemoteIpAddressToIPv4()
                    ?? context.Request.HttpContext.GetRemoteIpAddressToIPv6(),
                context.Request.Method,
                context.Request.Headers.Host,
                context.Request.Path.Value,
                context.Response.StatusCode,
                context.Response.ContentType,
                context.Response.ContentLength??0
            )
        );

        Console.ResetColor();
    }
}
