using XH_Server.Application.Services.Application.Approval;
using XH_Server.Application.Services.System;
using XH_Server.Core;

namespace XH_Server.Application.Utils;

public static class ServiceExtra
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddSingleton(typeof(Repository<>));
        return services;
    }

    public static IServiceCollection AddSystemService(this IServiceCollection services)
    {
        services.AddSingleton<ISystemService, SystemService>();
        return services;
    }

    public static IServiceCollection AddApprovalService(this IServiceCollection services)
    {
        services.AddSingleton<IApprovalService, ApprovalService>();
        return services;
    }

}
