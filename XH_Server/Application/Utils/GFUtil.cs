using XH_Server.Application.Utils;
using XH_Server.Core;

namespace XH_Server.Applications.Utils;

public static class GFUtil
{
    public static WebApplicationBuilder GFInject(this WebApplicationBuilder builder)
    {
        builder.Services.AddSqlSugarSetup(builder.Configuration);
        builder.Services.AddRepository();
        builder.Services.AddSystemService();


        return builder;
    }
}
