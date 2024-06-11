using System.Data;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using Server.Entry.Utils;
using Utils;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 耗材
/// </summary>
public class ConsumableManagement(BaseFunc DingTalk)
    : BasicApplicationApi<EConsumableLog>,
        IDynamicApiController
{
    /// <summary>
    /// 上传耗材表
    /// </summary>
    /// <param name="coperId">上传人</param>
    /// <param name="spaceId">空间Id</param>
    /// <param name="fileId">文件Id</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<ActionResult> PostFile(
        string coperId,
        string spaceId,
        string fileId,
        string fileName
    )
    {
        var unioId = DingTalk.GetUserUid(coperId);
        var (url, authorization, date) = DingTalk.GetFileDownload(unioId, spaceId, fileId);
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        client.DefaultRequestHeaders.Add("x-oss-date", date);

        var res = await client.GetAsync(url);

        using var fileStream = new MemoryStream();
        (await res.Content.ReadAsStreamAsync()).CopyTo(fileStream);
        fileStream.Position = 0;

        var data = ExcelUtils.ExcelToTable(fileStream, fileName) ?? throw new Exception("获取数据失败");
        var entity = new List<EConsumableManagement>();
        foreach (DataRow row in data.Rows)
        {
            const string ERR = "ERROR";
            var consumable = new EConsumableManagement
            {
                IsDeleted = false,

                Code = row["编号"].ToString() ?? ERR,
                Name = row["名称"].ToString() ?? ERR,
                Level = row["等级"].ToString() ?? ERR,
                Usage = row["用途"].ToString() ?? ERR,
                Remake = row["备注"].ToString() ?? ERR,

                Price = double.TryParse(row["单价"].ToString(), out double price) ? price : default,

                Count = int.TryParse(row["数量"].ToString(), out int count) ? count : default,

                TotalPrice = double.TryParse(row["总价"].ToString(), out double totalPrice)
                    ? totalPrice
                    : default,
            };

            entity.Add(consumable);
        }

        return new OkObjectResult(Db.Instance.Insertable(entity).ExecuteReturnSnowflakeIdList());
    }

    /// <summary>
    /// 获取当前耗材数据
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetConsumable()
    {
        var data = await Db
            .Instance.Queryable<EConsumableManagement>()
            .GroupBy(it => new { it.Name })
            .Select(it => new
            {
                it.Name,
                it.Level,
                it.Remake,
                Count = SqlFunc.AggregateSumNoNull(it.Count)
            })
            .ToListAsync();

        return new OkObjectResult(data);
    }

    /// <summary>
    /// 使用耗材
    /// </summary>
    /// <param name="name">耗材名称</param>
    /// <param name="count">使用数量</param>
    /// <param name="coperId">使用人</param>
    /// <param name="usage">用途</param>
    /// <param name="level">等级</param>
    /// <returns></returns>
    /// <exception cref="Exception">
    /// 当数量不足时抛出异常
    /// </exception>
    [HttpPut]
    public async Task<ActionResult> UseConsumable(
        string name,
        int count,
        string coperId,
        string usage,
        string level
    )
    {
        #region 耗材使用日志审核创建
        var entity = new EConsumableLog()
        {
            ConsumableName = name,
            Count = count,
            CoperId = coperId,
            Usage = usage,
            ConsumableLevel = level
        };
        var id = BasicEntityService.Create(entity);
        ApprovedPolicyService.CreateApproveBasicLog(entity);
        var log = ApprovedPolicyService.GetCurrentApprovalLog(id);
        if (log != null)
        {
            DingTalkUtils.SendMsg([log.ApproverId.ToString()], $"有一个待审核的消息！\r\n数据ID：{entity.Id}");
        }
        #endregion

        #region 耗材数据更改
        var data = Db
            .Instance.Queryable<EConsumableManagement>()
            .Where(it => it.Name == name)
            .ToList();

        var cnt = data.Sum(it => it.Count);
        if (cnt < count)
            throw new Exception($"耗材不足！剩余量：{cnt}");

        data[0].Count -= count;

        var res = await Db.Instance.Updateable(data).ExecuteCommandAsync();
        #endregion

        return new OkObjectResult(new { Success = true, Data = res });
    }

    /// <summary>
    /// 获取耗材剩余记录
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetFile()
    {
        var data = await Db
            .Instance.Queryable<EConsumableManagement>()
            .GroupBy(it => it.Name)
            .Select(it => new EConsumableManagement()
            {
                Code = it.Code,
                Level = it.Level,
                Remake = it.Remake,
                Count = SqlFunc.AggregateSumNoNull(it.Count),
                TotalPrice = it.Price * it.Count,
                Price = it.Price,
                Name = it.Name,
                IsDeleted = it.IsDeleted,
            })
            .ToListAsync();

        var stream = new NPOIStream() { AllowClose = false };
        var file = new XSSFWorkbook();

        var sheet = file.CreateSheet();
        var header = sheet.CreateRow(0);

        string[] headerVal = ["编号", "名称", "等级", "用途", "单价", "数量", "总价", "备注"];

        foreach (var val in headerVal.Select((it, index) => (it, index)))
        {
            header.CreateCell(val.index).SetCellValue(val.it);
        }

        for (int i = 0; i < data.Count; i++)
        {
            var rowData = data[i];
            var row = sheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue(rowData.Code);
            row.CreateCell(1).SetCellValue(rowData.Name);
            row.CreateCell(2).SetCellValue(rowData.Level);
            row.CreateCell(3).SetCellValue(rowData.Remake);
            row.CreateCell(4).SetCellValue(rowData.Price);
            row.CreateCell(5).SetCellValue(rowData.Count);
            row.CreateCell(6).SetCellValue(rowData.TotalPrice);
        }

        file.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;

        return new FileStreamResult(
            stream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
    }
}
