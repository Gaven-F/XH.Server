using SqlSugar;
using System.Reflection;
using XH_Server.Domain.Entities;

namespace XH_Server.Application.Services.System;

public class SystemService : ISystemService
{
    private readonly ISqlSugarClient _db;

    public SystemService(ISqlSugarClient db) => _db = db;

    public void InitDataBase()
    {
        #region 初始化数据库
        _db.DbMaintenance.CreateDatabase();
        #endregion

        #region 清除原有数据
        _db.DbMaintenance.DropTable(
             (from t in _db.DbMaintenance.GetTableInfoList()
              select t.Name).ToArray());
        #endregion

        #region 创建实体表
        _db.CodeFirst.InitTables(
              (from t in Assembly.GetExecutingAssembly().GetTypes()
               where t.BaseType == typeof(BaseEntity)
               select t).ToArray());
        #endregion
    }
}
