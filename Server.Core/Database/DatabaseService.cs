using Server.Core.Config;
using SqlSugar;
using System.Reflection;

namespace Server.Core.Database;
public class DatabaseService
{
    private const string EntityEndChar = "E";

    public ISqlSugarClient Instance { get; private set; }

    public void InitDatabase() => Instance.DbMaintenance.CreateDatabase();

    public void InitTable(IEnumerable<Type> tables, bool clearData = true)
    {
        InitDatabase();

        if (clearData)
        {
            Instance.DbMaintenance.GetTableInfoList()
                .Select(it => it.Name)
                .ToList()
                .ForEach(tableName => Instance.DbMaintenance.DropTable(tableName));
        }

        Instance.CodeFirst.InitTables(tables.ToArray());
    }

    public DatabaseService(ConfigService config)
    {
        Instance = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = config.DatabaseConfig.ConnectionString,
            DbType = Enum.Parse<DbType>(config.DatabaseConfig.DatabaseType),
            IsAutoCloseConnection = true,
            ConfigureExternalServices = new ConfigureExternalServices()
            {
                EntityService = (propInfo, entity) =>
                {
                    if (!entity.IsPrimarykey
                        && new NullabilityInfoContext().Create(propInfo).WriteState is NullabilityState.Nullable)
                    {
                        entity.IsNullable = true;
                    }
                    entity.DbColumnName = char.ToLower(propInfo.Name[0]) + propInfo.Name[1..^0];
                },
                EntityNameService = (type, entity) =>
                {
                    if (type.Name.EndsWith(EntityEndChar))
                    {
                        entity.DbTableName = type.Name[..^EntityEndChar.Length];
                    }
                }


            }
        });
    }
}
