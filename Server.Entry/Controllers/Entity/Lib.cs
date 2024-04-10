using Furion.DynamicApiController;
using Server.Application.Entities;
using Server.Domain.Repository;

namespace Server.Web.Controllers.Entity;

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
    /// <param name="operate"></param>
    public void PostGoodsInfo(long id, string info, string operate)
    {
        var d = repository.GetDataById(id);
        d.Info = info;
        d.UpdateTime = DateTime.Now;
        d.Operate = operate;
        repository.UpdateData(d);
    }

    /// <summary>
    /// 获取样品操作设备列表
    /// </summary>
    /// <param name="sId"></param>
    /// <returns></returns>
    public IEnumerable<EEquipmentLog> GetEquipmentLogById(string sId)
    {
        var rawData = repository.GetData()
            .Where(it => it.BindS == sId && it.Type == "E")
            .OrderBy(it => it.CreateTime)
            .ToList();

        var res = new List<EEquipmentLog>();

        rawData.ForEach(d =>
        {
            var cache = res.Where(it => it.GoodsID == d.GoodsID).ToList();

            if (cache.Count > 0 && d.CreateTime > cache[0].CreateTime && d.CreateTime > cache[0].EndTime)
            {
                res[res.FindIndex(it => it.Id == cache[0].Id)].EndTime = d.CreateTime;
            }
            else
            {
                res.Add(d);
            }
        });

        return res;
    }
}
