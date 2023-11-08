using Furion.DependencyInjection;
using SqlSugar;
using System.Reflection;
using XH.Application.System.Services;
using XH.Core.DataBase.Tables;

namespace XH.Application;

public class SystemService : ISystemService, ITransient
{
    private readonly SqlSugarScope _db;

    public SystemService(ISqlSugarClient sqlSugarClient)
    {
        _db = (SqlSugarScope)sqlSugarClient;
    }

    public void DataBaseInit()
    {
        var tables = Assembly.GetAssembly(typeof(BaseTable))!.GetTypes()
            .Where(t => ReferenceEquals(t.BaseType, typeof(BaseTable)))
            .ToArray();


        _db.DbMaintenance.CreateDatabase();
        _db.CodeFirst.InitTables(tables);
    }

    public string GetDescription()
    {
        return "获取成功！";
    }
}
