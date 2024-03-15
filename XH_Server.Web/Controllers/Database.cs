using Microsoft.AspNetCore.Mvc;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Web.Controllers;


[ApiDescriptionSettings(Order = 99)]
public class Database(DatabaseService databaseService) : ControllerBase
{
    public void DatabaseInit(bool clearData = true)
    {
        databaseService.InitDatabase();
        List<Type> tables =
        [
            .. typeof(ELeave).Assembly.GetTypes().Where(t => t.BaseType == typeof(BasicEntity)).ToList(),
            .. typeof(BasicEntity).Assembly.GetTypes().Where(t => t.BaseType == typeof(BasicEntity)).ToList(),
        ];
        databaseService.InitTable(tables.Distinct(), clearData);
    }
}
