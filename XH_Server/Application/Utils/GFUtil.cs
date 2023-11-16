using Furion.Schedule;
using XH_Server.Application.Utils;
using XH_Server.Applications.Jobs;
using XH_Server.Core;

namespace XH_Server.Applications.Utils;

public static class GFUtil
{
    public static WebApplicationBuilder GFInject(this WebApplicationBuilder builder)
    {
        builder.Services.AddSqlSugarSetup(builder.Configuration);
        builder.Services.AddRepository();
        builder.Services.AddSystemService();

        // Application Services
        builder.Services.AddApprovalService();


        //builder.Services.AddSchedule(options =>
        //{
        //    options.LogEnabled = true;
        //    options.AddJob(typeof(DemoJob).ScanToBuilder());
        //});


        builder.Services.AddConsoleFormatter();

        return builder;
    }
}
