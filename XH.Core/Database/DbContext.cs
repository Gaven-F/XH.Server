using Furion;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XH.Core.DataBase;

/// <summary>
/// 数据库上下文对象
/// </summary>
public static class DbContext
{
    /// <summary>
    /// SqlSugar 数据库实例
    /// </summary>
    public static readonly SqlSugarScope Instance = new SqlSugarScope(
        // 读取 appsettings.json 中的 ConnectionConfigs 配置节点
        App.GetConfig<List<ConnectionConfig>>("ConnectionConfigs").Select(conf =>
        {
            conf.ConfigureExternalServices = new ConfigureExternalServices
            {
                EntityService = (propertyInfo, col) =>
                {
                    if (!col.IsPrimarykey
                        && new NullabilityInfoContext().Create(propertyInfo).WriteState is NullabilityState.Nullable)
                    {
                        col.IsNullable = true;
                    }
                }
            };
            return conf;
        }).ToList()
        , db =>
        {
            // 这里配置全局事件，比如拦截执行 SQL
        });

}
