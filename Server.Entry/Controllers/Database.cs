using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Controllers;

[ApiDescriptionSettings(Order = 99)]
public class Database(DatabaseService databaseService) : IDynamicApiController
{
    public void DatabaseInit(bool clearData = true)
    {
        databaseService.InitDatabase();
        List<Type> tables =
        [
            .. typeof(ELeave)
                .Assembly.GetTypes()
                .Where(t => t.BaseType == typeof(BasicEntity))
                .ToList(),
            .. typeof(BasicEntity)
                .Assembly.GetTypes()
                .Where(t => t.BaseType == typeof(BasicEntity))
                .ToList(),
        ];
        databaseService.InitTable(tables.Distinct(), clearData);
    }
}
