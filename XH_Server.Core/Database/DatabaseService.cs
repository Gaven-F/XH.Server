using SqlSugar;
using System.Reflection;
using XH_Server.Core.Config;

namespace XH_Server.Core.Database;
public class DatabaseService
{
	public ISqlSugarClient Instance { get; private set; }

	public DatabaseService(ConfigService config)
	{
		Instance = new SqlSugarScope(new ConnectionConfig()
		{
			ConnectionString = config.DatabaseConfig.ConnectionString,
			DbType = Enum.Parse<DbType>(config.DatabaseConfig.DatabaseType),
			IsAutoCloseConnection = true,
			InitKeyType = InitKeyType.Attribute,
			ConfigureExternalServices = new ConfigureExternalServices()
			{
				EntityService = (propInfo, entity) =>
				{
					if (!entity.IsPrimarykey
						&& new NullabilityInfoContext().Create(propInfo).WriteState is NullabilityState.Nullable)
					{
						entity.IsNullable = true;
					}
				}
			}
		});
	}
}
