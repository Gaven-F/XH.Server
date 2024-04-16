using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Entities;
using Server.Core.Database;
using Server.Domain.Repository;

namespace Server.Web.Controllers.Entity;

public class Lib(IRepositoryService<EEquipmentLog> repository, DatabaseService database) : IDynamicApiController
{
    /*
     * S为样品，E为设备
     * G为扫描枪
     */

    /// <summary>
    /// 提交扫码记录
    /// </summary>
    /// <param name="data"></param>
    public void PostEquipmentLog(string data)
    {
        var val = data.Split('|');
        var log = new EEquipmentLog { EquipmentId = val[0], GoodsID = val[1], };

        if (char.ToLower(val[1][0]) == 's')
        {
            log.Type = "S";
            if (!CheckGoodHas(database, log.GoodsID))
                return;
        }
        else if (char.ToLower(val[1][0]) == 'e')
        {
            // 若为设备，则查找最近的样品记录
            // 若没有找到，则记录错误
            // 若找到，则绑定样品
            log.Type = "E";
            var cacheData = repository.GetData(false);
            log.BindS =
                cacheData
                    .OrderByDescending(e => e.CreateTime)
                    .FirstOrDefault(it => it.Type == "S" && it.EquipmentId == val[0])
                    ?.GoodsID ??
                "ERROR";

            if (!CheckGoodHas(database, log.BindS))
                return;
        }
        repository.SaveData(log);
    }

    public int DeleteLog(string sId)
    {
        return database.Instance.Updateable<EEquipmentLog>()
            .Where(it => it.GoodsID == sId || it.BindS == sId)
            .SetColumns(it => new EEquipmentLog() { IsDeleted = true, UpdateTime = DateTime.Now })
            .ExecuteCommand();
    }

    private static bool CheckGoodHas(DatabaseService database, string goodsId)
    {
        // 判断样品订单是否存在以及是否结束
        return database.Instance
            .Queryable<EOrder>()
            .Where(
                order => order.Code == goodsId || ('s' + order.Code).Equals(goodsId, StringComparison.OrdinalIgnoreCase))
            .Where(order => !order.IsComplete)
            .Any();
    }

    /// <summary>
    /// 获取样品列表
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetGoodsId()
    {
        return repository
            .GetData(false)
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
        var res = new List<EEquipmentLog>();
        var cacheData = new Queue<EEquipmentLog>(
            repository
            .GetData()
            .Where(it => it.BindS == sId && it.Type == "E")
            .OrderBy(it => it.CreateTime));

        while (cacheData.Count > 0)
        {
            var data = cacheData.Dequeue();

            if (res.Count != 0 && res[^1].GoodsID == data.GoodsID)
            {
                res[^1].EndTime = data.CreateTime;
            }
            else
            {
                res.Add(data);
            }
        }

        return res;
    }
}
