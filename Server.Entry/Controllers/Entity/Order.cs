﻿using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 订单_V2
/// </summary>
public class Order : BasicApplicationApi<EOrder>, IDynamicApiController
{
    new private ISqlSugarClient Db { get => base.Db.Instance; }
    public override Results<Ok<string>, BadRequest<string>> Add(EOrder entity) =>
        TypedResults.Ok(Db
            .InsertNav(entity)
            .Include(it => it.Items)
            .ExecuteReturnEntity().ToString());

#pragma warning disable IDE0060 // 删除未使用的参数
    /// <summary>
    /// 获取所有订单数据
    /// </summary>
    /// <param name="null">无需传值</param>
    /// <returns></returns>
    public ActionResult<IEnumerable<EOrder>> GetData(string @null = "") =>
        BasicEntityService.GetEntities().ToList();

    /// <summary>
    /// 获取订单数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="null">无需传值</param>
    /// <returns></returns>
    public ActionResult<EOrder> GetDataById(string id, string @null = "") =>
        BasicEntityService.GetEntityById(Convert.ToInt64(id));

#pragma warning restore IDE0060 // 删除未使用的参数
    /// <summary>
    /// 订单更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult UpdateItem(string id, EOrderItem item)
    {
        {
            var _id = Convert.ToInt64(id);
            if (_id != item.Id)
            {
                return new BadRequestObjectResult("id不匹配！");
            }
            item.UpdateTime = DateTime.Now;
            var res = Db.Updateable(item).ExecuteCommand();
            return new OkObjectResult(res);
        }
    }

    [HttpGet]
    public IResult DownloadFile([FromServices] DatabaseService dbService, string orderId)
    {
        var id = Convert.ToInt64(orderId);
        var db = dbService.Instance;
        var data = db.Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted)
            .InSingle(id);

        if (data == null)
        {
            return Results.BadRequest();
        }
        ;
        var stream = Utils.OrderUtils.ReplaceByEntity(data);
        return TypedResults.Stream(
            stream,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "demo.docx"
        );
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
        var db = Db;

        var rawOrderData = db.Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted && it.IsComplete)
            .Where(it => it.Code.Contains(code))
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

        var rawLibData = new Queue<EEquipmentLog>(
            db.Queryable<EEquipmentLog>()
                .Where(it => !it.IsDeleted)
                .Where(it =>
                    orderData.Code.Contains(it.GoodsID) || orderData.Code.Contains(it.BindS ?? "")
                )
                .ToList()
        );

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

        var pOrder = orderData.DeepClone();
        var pLog = logData.DeepClone();

        pOrder.Items!.ForEach(i =>
        {
            if (logData.Count > 0)
            {
                var data = logData.Dequeue();
                i.StartTime = data.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                i.EndTime = data.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        });

        return new OkObjectResult(
            new
            {
                pOrder,
                pLog,
                rawOrderData
            }
        );
    }

    [NonAction]
    public override Results<Ok<List<Tuple<EOrder, EApprovalLog>>>,
        BadRequest<string>> GetData()
    {
        try
        {
            var data = Db
                .Queryable<EOrder>()
                .Where(it => !it.IsDeleted)
                .Includes(it => it.Items)
                .ToList();
            List<Tuple<EOrder, EApprovalLog>> res = new(data.Count);
            foreach (var entity in data)
            {
                var log = ApprovedPolicyService.GetCurrentApprovalLog(entity.Id);
                res.Add(new(entity, log));
            }
            return TypedResults.Ok(res);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }


    [NonAction]
    public override Results<Ok<Tuple<EOrder, EApprovalLog>>, BadRequest<string>> GetDataById(
        long id
    )
    {
        var data = Db
            .Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted)
            .InSingle(id);

        if (data == null)
        {
            return TypedResults.BadRequest("未找到实例！");
        }

        var log = ApprovedPolicyService.GetCurrentApprovalLog(id);

        return TypedResults.Ok(new Tuple<EOrder, EApprovalLog>(data, log));
    }


}
