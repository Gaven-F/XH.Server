using Furion.DependencyInjection;
using SqlSugar;
using System.Reflection;
using XH.Application.System.Services;
using XH.Core.DataBase.Entities;

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
		var entities = Assembly.GetAssembly(typeof(BaseEntity))!.GetTypes()
			.Where(t => ReferenceEquals(t.BaseType, typeof(BaseEntity)))
			.ToArray();

		_db.DbMaintenance.CreateDatabase();

		// 清除所有原有数据
		//var t = _db.DbMaintenance.GetTableInfoList();
		//t.Select(it => it.Name).ToList()
		//    .ForEach(it => _db.DbMaintenance.DropTable(it));

		_db.CodeFirst.InitTables(entities);

		// 添加种子数据
		AddSeedData();
	}

	public string GetDescription()
	{
		return "获取成功！";
	}
}
