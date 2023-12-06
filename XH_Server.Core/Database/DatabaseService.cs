﻿using SqlSugar;
using System.Reflection;
using XH_Server.Core.Config;

namespace XH_Server.Core.Database;
public class DatabaseService
{
	public ISqlSugarClient Instance { get; private set; }

	public void InitDatabase() => Instance.DbMaintenance.CreateDatabase();

	public void InitTable(IEnumerable<Type> tables)
	{
		InitDatabase();

		Instance.DbMaintenance.GetTableInfoList()
			.Select(it => it.Name)
			.ToList()
			.ForEach(tableName => Instance.DbMaintenance.DropTable(tableName));

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
				}
			}
		});
	}
}
