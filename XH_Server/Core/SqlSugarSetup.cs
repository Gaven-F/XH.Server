using SqlSugar;
using System.Reflection;

namespace XH_Server.Core;

public static class SqlSugarSetup
{
    public static IServiceCollection AddSqlSugarSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionConfig = configuration.Get<ConnectionConfig>("ConnectionConfig") ?? throw new Exception("未找到数据库配置");

        connectionConfig.ConfigureExternalServices = new ConfigureExternalServices
        {
            EntityService = (propInfo, col) =>
            {
                if (!col.IsPrimarykey && new NullabilityInfoContext().Create(propInfo).WriteState is NullabilityState.Nullable)
                {
                    col.IsNullable = true;
                }
            },
            EntityNameService = (type, entity) => {
                entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName.ToLower());
            }
        };

        var sqlsugarScope = new SqlSugarScope(connectionConfig, (db) =>
        {

        });

        services.AddSingleton<ISqlSugarClient>(sqlsugarScope);

        return services;
    }
}
