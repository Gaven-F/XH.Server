using Furion.DynamicApiController;
using XH_Server.Application.Entities;
using XH_Server.Domain.Repository;

namespace XH_Server.Web.Controllers.Entity;

public class Lib(IRepositoryService<EEquipmentLog> repository) : IDynamicApiController
{
	/// <summary>
	/// 提交扫码记录
	/// </summary>
	/// <param name="data"></param>
	public void PostEquipmentLog(string data)
	{
		var val = data.Split('|');
		var log = new EEquipmentLog
		{
			EquipmentId = val[0],
			GoodsID = val[1],
		};

		if (char.ToLower(val[1][0]) == 's')
		{
			log.Type = "S";
		}
		else if (char.ToLower(val[1][0]) == 'e')
		{
			log.Type = "E";
			var cacheData = repository.GetData(false);
			log.BindS = cacheData.OrderByDescending(e => e.CreateTime)
				.FirstOrDefault(it => it.Type == "S" && it.EquipmentId == val[0])?.GoodsID ?? "ERROR";
		}
		repository.SaveData(log);
	}

	/// <summary>
	/// 获取样品列表
	/// </summary>
	/// <returns></returns>
	public IEnumerable<string> GetGoodsId()
	{
		return repository.GetData(false)
			.Where(it => it.Type == "S")
			.Select(it => it.GoodsID)
			.Distinct()
			.ToList();
	}

	/// <summary>
	/// 提交补充信息
	/// </summary>
	/// <param name="id"></param>
	/// <param name="info"></param>
	public void PostGoodsInfo(long id, string info)
	{
		var d = repository.GetDataById(id);
		d.Info = info;
		d.UpdateTime = DateTime.Now;
		repository.UpdateData(d);
	}
}
