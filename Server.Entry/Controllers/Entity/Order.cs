using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Mapster;
using Masuit.Tools;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NPOI.Util;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Core.Database;
using Server.Domain.ApprovedPolicy;
using System.Dynamic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 订单_V2
/// </summary>
public class Order : BasicApplicationApi<EOrder, EOrder>, IDynamicApiController
{
    public override Results<Ok<string>, BadRequest<string>> Add(EOrder entity)
    {
        try
        {
            var id = BasicEntityService
                .GetDb()
                .Instance
                .InsertNav(entity)
                .Include(it => it.Items)
                .ExecuteReturnEntity()
                .Id;
            ApprovedPolicyService.CreateApproveBasicLog(entity);
            var log = ApprovedPolicyService.GetCurrentApprovalLog(id);
            if (log != null)
            {
                DingTalkUtils.SendMsg([log.ApproverId.ToString()], $"有一个待审核的消息！\r\n数据ID：{entity.Id}");
            }

            return TypedResults.Ok(id.ToString());
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public override Results<Ok<List<Tuple<EOrder, EApprovalLog>>>, BadRequest<string>> GetData()
    {
        try
        {
            var data = Db
                .Instance
                .Queryable<EOrder>()
                .Where(it => !it.IsDeleted)
                .Includes(it => it.Items)
                .ToList();
            List<Tuple<EOrder, EApprovalLog>> res = new(data.Count);
            foreach (var entity in data)
            {
                var log = ApprovedPolicyService
                    .GetCurrentApprovalLog(entity.Id)
                    .Adapt<Vo.ApproLog>();
                res.Add(new(entity, log));
            }
            return TypedResults.Ok(res);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    [HttpGet("{engineerId}")]
    public ActionResult<IEnumerable<EOrder>> GetDataByEngineer(string engineerId)
    {
        var data = BasicEntityService
            .GetDb()
            .Instance
            .Queryable<EOrder>()
            .Where(it => !it.IsDeleted)
            .Includes(it => it.Items)
            .ToList();
        var res = data.Where(d => d.Items != null && d.Items.Where(i => i.Engineer == engineerId).Any());
        return res.ToList();
    }

    public override Results<Ok<Tuple<EOrder, Vo.ApproLog>>, BadRequest<string>> GetDataById(long id)
    {
        var data = BasicEntityService
            .GetDb()
            .Instance
            .Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted)
            .InSingle(id);

        if (data == null)
        {
            return TypedResults.BadRequest("未找到实例！");
        }

        var log = ApprovedPolicyService.GetCurrentApprovalLog(id);

        return TypedResults.Ok(new Tuple<EOrder, Vo.ApproLog>(data, log.Adapt<Vo.ApproLog>()));
    }

    [HttpPut("{id}")]
    public ActionResult UpdateItem(string id, EOrderItem item)
    {
        var db = BasicEntityService.GetDb();
        var _id = Convert.ToInt64(id);
        if (_id != item.Id)
        {
            return new BadRequestObjectResult("id不匹配！");
        }
        item.UpdateTime = DateTime.Now;
        var res = db.Instance.Updateable(item).ExecuteCommand();
        return new OkObjectResult(res);
    }

    [HttpGet]
    public IResult DownloadFile([FromServices] DatabaseService dbService, string orderId)
    {
        var id = Convert.ToInt64(orderId);
        var db = dbService.Instance;
        var data = db.Queryable<EOrder>().Includes(it => it.Items).Where(it => !it.IsDeleted).InSingle(id);

        if (data == null)
        {
            return Results.BadRequest();
        }
        ;
        var stream = Utils.OrderUtils.ReplaceByEntity(data);
        return TypedResults.Stream(
            stream,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "demo.docx");
    }

    public ActionResult GetDataByName(string name)
    {
        var data = Db
            .Instance
            .Queryable<EOrder>()
            .Where(it => !it.IsDeleted)
            .Includes(it => it.Items)
            .ToList();

        var res = data
            .Where(d => d.Items != null && d.ProductsName == name);

        return new OkObjectResult(res);
    }

    public ActionResult GetDataByCode(string code)
    {
        var data = Db
            .Instance
            .Queryable<EOrder>()
            .Where(it => !it.IsDeleted)
            .Includes(it => it.Items)
            .ToList();

        var res = data
            .Where(d => d.Items != null && d.Code == code);

        return new OkObjectResult(res);
    }

    public void CompleteOrder(string orderId)
    {
        var id = Convert.ToInt32(orderId);
        var order = Db.Instance.Queryable<EOrder>().Includes(it => it.Items).InSingle(id);

        order.IsComplete = true;
        order.UpdateTime = DateTime.Now;

        Db.Instance.Updateable(order).ExecuteCommand();
    }

    public void DeleteOrder(string orderId)
    {
        var id = Convert.ToInt32(orderId);
        var order = Db.Instance.Queryable<EOrder>().Includes(it => it.Items).InSingle(id);

        order.IsDeleted = true;
        order.UpdateTime = DateTime.Now;

        Db.Instance.Updateable(order).ExecuteCommand();
    }

    /// <summary>
    /// 获取订单记录
    /// </summary>
    /// <remarks>
    /// *注：*此订单获取数据应当在订单完成后进行调用  
    /// </remarks>
    /// <param name="code">
    /// 查询订单样品绑定代码
    /// </param>
    public ActionResult GetOrderWithLibLog(string code)
    {
        var db = Db.Instance;

        var rawOrderData = db
            .Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted && it.IsComplete)
            .Where(it => it.Code == code)
            .ToList();

        if (rawOrderData.Count > 1)
        {
            return new BadRequestObjectResult("绑定Id并不唯一！无法获取正常数据");
        }

        var orderData = rawOrderData.FirstOrDefault()!;

        if (orderData.Items == null)
        {
            return new BadRequestObjectResult("订单无制作工艺！");
        }

        var rawLibData = new Queue<EEquipmentLog>(db
            .Queryable<EEquipmentLog>()
            .Where(it => !it.IsDeleted)
            .Where(it => it.GoodsID == orderData.Code ||
                it.BindS == orderData.Code)
            .ToList());

        var logData = new Queue<EEquipmentLog>();

        while (rawLibData.Count > 0)
        {
            var data = rawLibData.Dequeue();

            if (logData.Count != 0 && logData.Peek().GoodsID == data.GoodsID)
            {
                logData.Peek().EndTime = data.CreateTime;
            }
            else
            {
                logData.Enqueue(data);
            }
        }

        var pOrder= orderData.DeepClone();
        var pLog = logData.DeepClone();

        pOrder.Items!.ForEach(i =>
        {
            if(logData.Count > 0)
            {
                var data = logData.Dequeue();
                i.StartTime = data.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                i.EndTime = data.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        });

        return new OkObjectResult(new {pOrder, pLog, rawOrderData });
    }
}
