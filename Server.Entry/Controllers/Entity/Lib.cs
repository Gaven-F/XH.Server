using Furion.DynamicApiController;
using NPOI.Util;
using SharpCompress;

namespace Server.Web.Controllers.Entity;

public class Lib(IRepositoryService<EEquipmentLog> repository, DatabaseService database) : IDynamicApiController
{
    private ISqlSugarClient Db => database.Instance;

    public void StopLog(string sCode)
    {
        if (Db.Queryable<EStopEquipment>().Any(it => it.StopCode == sCode))
            throw new Exception("订单已完成！无法再次操作！");

        var order = Db.Queryable<EOrder>()
            .Includes(it => it.Items)
            .ToList().Find(it => it.Code.Contains(sCode))!;

        if (order == null)
        {
            throw new Exception("无数据！");
        }
        else if (order.CompleteOrderId != 0)
        {
            throw new Exception("订单已完成！无法再次操作！");
        }

        Db.Insertable(new EStopEquipment { StopCode = sCode }).ExecuteReturnSnowflakeId();

        var complete =
            Db.Queryable<EStopEquipment>()
            .Where(it => order.Code.Contains(it.StopCode))
            .ToList()
            .Count;

        CompleteOrder(order, complete >= order.Code.Count);
    }

    private void CompleteOrder(EOrder order, bool complete)
    {
        if (!complete)
        {
            return;
        }

        var codes = order.Code;

        var logs = new Queue<EEquipmentLog>(
            Db.Queryable<EEquipmentLog>().Where(it => codes.Contains(it.BindS ?? it.GoodsID) && it.Type == "E").ToList());

        var processedLogs = new Stack<EEquipmentLog>();

        while (logs.Count > 0)
        {
            var log = logs.Dequeue();
            if (processedLogs.Count > 0 && processedLogs.Peek()?.GoodsID == log.GoodsID)
            {
                processedLogs.Peek().EndTime = log.CreateTime;
            }
            else
            {
                processedLogs.Push(log);
            }
        }

        var cnt = 0;
        var completeOrder = order.DeepClone();
        completeOrder.Items = order.Items.DeepClone();
        completeOrder.Id = 0;

        while (processedLogs.Count > 0)
        {
            var log = processedLogs.Pop();
            completeOrder.Items ??= [];
            if (cnt >= completeOrder.Items.Count)
            {
                completeOrder.Items.Add(new()
                {
                    DeviceNumber = log.GoodsID,
                    StartTime = log.CreateTime.ToString(),
                    EndTime = log.EndTime.ToString(),
                    Engineer = log.EquipmentId,
                    Info = log.Info ?? "",
                    Annex = log.Annex??"",
                    Operate = log.Operate??"",
                });
            }
            else
            {
                completeOrder.Items[cnt].DeviceNumber = log.GoodsID;
                completeOrder.Items[cnt].DeviceNumber = log.GoodsID;
                completeOrder.Items[cnt].StartTime = log.CreateTime.ToString();
                completeOrder.Items[cnt].EndTime = log.EndTime.ToString();
                completeOrder.Items[cnt].Engineer = log.EquipmentId;
                completeOrder.Items[cnt].Info = log.Info ?? "";
                completeOrder.Items[cnt].Id = 0;
                completeOrder.Items[cnt].Operate = log.Operate ?? "";
                completeOrder.Items[cnt].Annex = log.Annex ?? "";
            }
            cnt++;
        }

        Db.InsertNav(completeOrder)
            .Include(it => it.Items)
            .ExecuteCommand();

        order.CompleteOrderId = completeOrder.Id;
        order.UpdateTime = DateTime.Now;
        completeOrder.CompleteOrderId = completeOrder.Id;
        completeOrder.UpdateTime = DateTime.Now;
        completeOrder.CreateTime = DateTime.Now;

        Db.Updateable(new List<EOrder> { order, completeOrder })
            .UpdateColumns(it => new { it.CompleteOrderId, it.UpdateTime, it.CreateTime })
            .ExecuteCommand();
    }

    /// <summary>
    /// 提交扫码记录
    /// </summary>
    /// <param name="data"></param>
    public void PostEquipmentLog(string data)
    {
        //S为样品，E为设备 G为扫描枪
        var val = data.Split('|');
        var log = new EEquipmentLog { EquipmentId = val[0], GoodsID = val[1], };

        if (char.ToLower(val[1][0]) == 's')
        {
            log.Type = "S";
        }
        else if (char.ToLower(val[1][0]) == 'e')
        {
            // 若为设备，则查找最近的样品记录 若没有找到，则记录错误 若找到，则绑定样品
            log.Type = "E";
            var cacheData = repository.GetData(false);
            log.BindS =
                cacheData
                    .OrderByDescending(e => e.CreateTime)
                    .FirstOrDefault(it => it.Type == "S" && it.EquipmentId == val[0])
                    ?.GoodsID ??
                "ERROR";
        }
        repository.SaveData(log);
    }

    public int DeleteLog(string sId)
    {
        return database
            .Instance
            .Updateable<EEquipmentLog>()
            .Where(it => it.GoodsID == sId || it.BindS == sId)
            .SetColumns(it => new EEquipmentLog() { IsDeleted = true, UpdateTime = DateTime.Now })
            .ExecuteCommand();
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
    /// <param name="annex"></param>
    public void PostGoodsInfo(long id, string info, string operate, string annex)
    {
        var d = repository.GetDataById(id);
        d.Info = info;
        d.UpdateTime = DateTime.Now;
        d.Operate = operate;
        d.Annex = annex;
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
