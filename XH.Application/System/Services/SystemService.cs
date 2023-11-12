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

	public void AddSeedData()
	{
		
	}

	public void DataBaseInit()
	{
		var tables = Assembly.GetAssembly(typeof(BaseEntry))!.GetTypes()
			.Where(t => ReferenceEquals(t.BaseType, typeof(BaseEntry)))
			.ToArray();

		_db.DbMaintenance.CreateDatabase();

		// 清除所有原有数据
		var t = _db.DbMaintenance.GetTableInfoList();
		t.Select(it => it.Name).ToList()
			.ForEach(it => _db.DbMaintenance.DropTable(it));

		_db.CodeFirst.InitTables(tables);

		// 添加种子数据
		AddSeedData();
	}

	public string GetDescription()
	{
		return "获取成功！";
	}
}
