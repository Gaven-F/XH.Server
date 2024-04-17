using Furion.DynamicApiController;

namespace Server.Web.Controllers.Entity;

public class Lib(IRepositoryService<EEquipmentLog> repository, DatabaseService database)
    : IDynamicApiController
{
    private ISqlSugarClient Db => database.Instance;
    public void StopLog(string sCode)
    {
        Db.Insertable(new EStopEquipment { StopCode = sCode }).ExecuteReturnSnowflakeId();

        var order = Db.Queryable<EOrder>()
            .ToList()
            .Find(it => it.Code.Contains(sCode))!;

        if (order == null)
        {
            throw new Exception("无数据！");
        }
        else if (order.IsComplete)
        {
            throw new Exception("订单已完成！无法再次操作！");
        }

        bool complete =
            Db.Queryable<EStopEquipment>()
            .Any(it => order.Code.Contains(it.StopCode));

        CompleteOrder(order, complete);
    }

    private void CompleteOrder(EOrder order, bool complete)
    {
        if (!complete) return;

        order.IsComplete = true;
        order.UpdateTime = DateTime.Now;
        Db.Updateable(order).ExecuteCommand();

        var codes = order.Code;

        var logs = new Queue<EEquipmentLog>(Db.Queryable<EEquipmentLog>()
            .Where(it => codes.Contains(it.BindS ?? it.GoodsID) && it.Type == "E")
            .ToList());

        var processedLogs = new Queue<EEquipmentLog>();

        while (logs.Count > 0)
        {
            var log = logs.Dequeue();
            if (processedLogs.Count > 0 && processedLogs.Peek()?.GoodsID == log.GoodsID)
            {
                processedLogs.Peek().EndTime = log.CreateTime;
            }
            else
            {
                processedLogs.Enqueue(log);
            }
        }

        var cnt = 0;

        while (processedLogs.Count > 0)
        {
            var log = processedLogs.Dequeue();
            order.Items ??= [];
            if (cnt >= order.Items.Count)
            {
                order.Items.Add(new()
                {
                    DeviceNumber = log.GoodsID,
                    StartTime = log.CreateTime.ToString(),
                    EndTime = log.EndTime.ToString(),
                    Engineer = log.EquipmentId,
                    Info = log.Info ?? ""
                });
            }else
            {
                order.Items[cnt].DeviceNumber = log.GoodsID;
                order.Items[cnt].DeviceNumber = log.GoodsID;
                order.Items[cnt].StartTime = log.CreateTime.ToString();
                order.Items[cnt].EndTime = log.EndTime.ToString();
                order.Items[cnt].Engineer = log.EquipmentId;
                order.Items[cnt].Info = log.Info ?? "";
            }
            cnt++;
        }

        Db.Updateable(order).ExecuteCommand();
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
                    ?.GoodsID ?? "ERROR";
        }
        repository.SaveData(log);
    }

    public int DeleteLog(string sId)
    {
        return database
            .Instance.Updateable<EEquipmentLog>()
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
                .OrderBy(it => it.CreateTime)
        );

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
