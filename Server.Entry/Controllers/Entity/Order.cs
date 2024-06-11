using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Furion.DynamicApiController;
using Masuit.Tools.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using Server.Entry.Utils;
using Utils;
using TUPLE = System.Tuple<
    Server.Application.Entities.EOrder,
    Server.Domain.ApprovedPolicy.EApprovalLog
>;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 订单_V2
/// </summary>
public class Order(BaseFunc baseFunc) : BasicApplicationApi<EOrder>, IDynamicApiController
{
    private ISqlSugarClient Database => Db.Instance;

    public override Results<Ok<string>, BadRequest<string>> Add(EOrder entity) =>
        TypedResults.Ok(
            Database.InsertNav(entity).Include(it => it.Items).ExecuteReturnEntity().ToString()
        );

#pragma warning disable IDE0060 // 删除未使用的参数

    /// <summary>
    /// 获取所有订单数据
    /// </summary>
    /// <param name="null"></param>
    /// <returns></returns>
    public ActionResult GetData(string? @null = "null")
    {
        var data = Database
            .Queryable<EOrder>()
            //.Includes(it => it.Items.C_Where(i => !i.IsDeleted))
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted)
            .ToList();
        List<TUPLE> res = new(data.Count);
        foreach (var entity in data)
        {
            var log = ApprovedPolicyService.GetCurrentApprovalLog(entity.Id);
            res.Add(new(entity, log));
        }
        return new OkObjectResult(res);
    }

    /// <summary>
    /// 获取订单数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="null"></param>
    /// <returns></returns>
    public ActionResult<EOrder> GetDataById(string id, string? @null = "null") =>
        Database
            .Queryable<EOrder>()
            .Where(it => !it.IsDeleted)
            .Includes(it => it.Items)
            .InSingle(Convert.ToInt64(id));

#pragma warning restore IDE0060 // 删除未使用的参数
    /// <summary>
    /// 订单更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult UpdateItem(string id, EOrderItem item)
    {
        var _id = Convert.ToInt64(id);
        if (_id != item.Id)
        {
            return new BadRequestObjectResult("id不匹配！");
        }
        item.UpdateTime = DateTime.Now;
        var res = Database.Updateable(item).ExecuteCommand();
        return new OkObjectResult(res);
    }

    [HttpPost]
    public ActionResult ApprovalNewOrder(EOrder order)
    {
        order.Id = 0;
        order.Items?.ForEach(it => it.Id = 0);
        order.UpdateTime = DateTime.Now;
        order.StartApprove = true;
        order.UpdateTime = DateTime.Now;

        var res = Database.InsertNav(order).Include(it => it.Items).ExecuteCommand();

        ApprovedPolicyService.CreateApproveBasicLog(order);
        var log = ApprovedPolicyService.GetCurrentApprovalLog(order.Id);
        if (log != null)
        {
            DingTalkUtils.SendMsg([log.ApproverId.ToString()], $"有一个待审核的消息！\r\n数据ID：{order.Id}");
        }

        return new OkObjectResult(res);
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
        var stream = Utils.OrderUtils.ReplaceByEntity(data, baseFunc);
        return TypedResults.Stream(
            stream,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "demo.docx"
        );
    }

    [HttpGet, Obsolete("由WorkOrder替代")]
    public ActionResult DownloadFile()
    {
        var data = Db.Instance.Queryable<EOrder>().Where(it => !it.IsDeleted).ToArray();

        var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet();

        var header = sheet.CreateRow(0);
        string[] headerVal = ["编号", "随工单单号", "客户名称", "样品名称", "产品数量", "产品型号", "产品批次", "总时间", "总价格"];
        for (int i = 0; i < headerVal.Length; i++)
        {
            header.CreateCell(i).SetCellValue(headerVal[i]);
        }

        for (int i = 0; i < data.Length; i++)
        {
            var item = data[i];
            var row = sheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue(item.Numbering);
            row.CreateCell(1).SetCellValue(item.WorkNumber);
            row.CreateCell(2).SetCellValue(item.CustomerName);
            row.CreateCell(3).SetCellValue(item.ProductsName);
            row.CreateCell(4).SetCellValue(item.ProductsNumber);
            row.CreateCell(5).SetCellValue(item.ProductsModel);
            row.CreateCell(6).SetCellValue(item.ProductsLots);
            row.CreateCell(7).SetCellValue(item.TotalTime);
            row.CreateCell(8).SetCellValue(item.TotalPrice);
        }

        var stream = new NPOIStream(false);
        workbook.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;

        return new FileStreamResult(stream, MimeMapper.MimeTypes[".xlsx"]);
    }

    // 台账导出
    [HttpGet]
    public IResult DownloadWorkOrder()
    {
        var data = Db
            .Instance.Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted)
            .ToArray();
        //var data = Database
        //	.Queryable<EOrder>()
        //	.Includes(it => it.Items)
        //	.Where(it => !it.IsDeleted)
        //	.InSingle(id);
        //var data = Db.Instance.Queryable<EOrder>().Where(it => !it.IsDeleted).ToArray();

        var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet();

        var header = sheet.CreateRow(0);
        //string[] headerVal = ["编号", "随工单单号", "客户名称", "样品名称", "产品数量", "产品型号", "产品批次", "总时间", "总价格"];
        string[] headerVal =
        [
            "序号",
            "预计完成时间",
            "延期原因",
            "最终预计完成时间",
            "检测条件",
            "检测数量",
            "样品编号",
            "检测结果",
            "设备编号",
            "开始时间",
            "结束时间",
            "检测用时",
            "检测人员",
            "复核签字",
            "评分因子",
            "延期时间",
            "最终完成时间"
        ];
        for (int i = 0; i < headerVal.Length; i++)
        {
            header.CreateCell(i).SetCellValue(headerVal[i]);
        }
        /*
         EstimatedTime[0]	ReasonExtension[0]	FinalTime[0]	Condition[0]	Quantity[0]	SampleNumber[0]	TestResult[0]	DeviceNumber[0]
        StartTime[0]	EndTime[0]	StartTime[0]-EndTime[0]	Engineer[0]	Sign[0]	ScoringFactor[0]
        EndTime[0]-FinalTime[0]	EndTime[0]
         */
        if (data.Length <= 0)
        {
            UTILS.ThrowNullExp();
        }
        var rowCnt = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].Items == null)
            {
#pragma warning disable
                continue;
#pragma warning restore
            }
            for (int j = 0; j < data[i].Items!.Count; j++)
            {
                var item = data[i].Items![j];
                var row = sheet.CreateRow(rowCnt + 1);
                row.CreateCell(0).SetCellValue(j + 1);
                row.CreateCell(1).SetCellValue(item.EstimatedTime);
                row.CreateCell(2).SetCellValue(item.ReasonExtension);
                row.CreateCell(3).SetCellValue(item.FinalTime);
                row.CreateCell(4).SetCellValue(item.Condition);
                row.CreateCell(5).SetCellValue(item.Quantity);
                row.CreateCell(6).SetCellValue(item.SampleNumber);
                row.CreateCell(7).SetCellValue(item.TestResult);
                row.CreateCell(8).SetCellValue(item.DeviceNumber);
                row.CreateCell(9).SetCellValue(DateTime.Parse(item.StartTime).ToString("G"));
                row.CreateCell(10).SetCellValue(DateTime.Parse(item.EndTime).ToString("G"));
                row.CreateCell(11)
                    .SetCellValue(
                        (DateTime.Parse(item.StartTime) - DateTime.Parse(item.EndTime)).Hours
                    );
                row.CreateCell(12).SetCellValue(UTILS.GetUserNameById(baseFunc, item.Engineer));
                row.CreateCell(13).SetCellValue(item.Sign);
                row.CreateCell(14).SetCellValue(item.ScoringFactor);
                row.CreateCell(15)
                    .SetCellValue((DateTime.Parse(item.EndTime) - item.FinalTime).Hours);
                row.CreateCell(16).SetCellValue(item.FinalTime.ToString("G"));

                rowCnt++;
            }
        }

        var stream = new NPOIStream(false);
        workbook.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;
        return TypedResults.Stream(
            stream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"台账{DateTime.UtcNow.ToFileTime()}.xlsx"
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
    [NonAction]
    public ActionResult GetOrderWithLibLog(string code)
    {
        var db = Database;

        var rawOrderData = db.Queryable<EOrder>()
            .Includes(it => it.Items)
            .Where(it => !it.IsDeleted && it.CompleteOrderId != 0)
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

    //[NonAction]
    //public override Results<Ok<List<TUPLE>>, BadRequest<string>> GetData()
    //{
    //    try
    //    {
    //        var data = Database
    //            .Queryable<EOrder>()
    //            .Where(it => !it.IsDeleted)
    //            .Includes(it => it.Items)
    //            .ToList();
    //        List<TUPLE> res = new(data.Count);
    //        foreach (var entity in data)
    //        {
    //            var log = ApprovedPolicyService.GetCurrentApprovalLog(entity.Id);
    //            res.Add(new(entity, log));
    //        }
    //        return TypedResults.Ok(res);
    //    }
    //    catch (Exception e)
    //    {
    //        return TypedResults.BadRequest(e.Message);
    //    }
    //}

    [NonAction]
    public override Results<Ok<TUPLE>, BadRequest<string>> GetDataById(long id)
    {
        var data = Database
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

    [HttpPost]
    public ActionResult AddItem([FromQuery] string orderId, [FromBody] EOrderItem item)
    {
        item.OrderId = Convert.ToInt64(orderId);
        Db.Instance.Insertable(item).ExecuteReturnSnowflakeId();
        return new OkResult();
    }
}
