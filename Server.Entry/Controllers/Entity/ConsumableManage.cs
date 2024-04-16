using Furion.DynamicApiController;
using MathNet.Numerics.Distributions;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Server.Domain.Basic;
using Server.Entry.Utils;
using System.Data;

namespace Server.Entry.Controllers.Entity;

public class ConsumableManage : IDynamicApiController
{
    [HttpPost, NonUnify]
    public ActionResult Add(IFormFile file)
    {
        var data = new List<EConsumableManage>();
        IWorkbook workbook;
        string fileExt = Path.GetExtension(file.FileName).ToLower();
        using (var fs = file.OpenReadStream())
        {
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook(fs);
            }
            else
            {
                return new BadRequestObjectResult("文件格式错误！\r\n仅接收xlsx类型文件！");
            }

            var sheet = workbook.GetSheetAt(0);

            var header = sheet.GetRow(sheet.FirstRowNum);

            var cellMsgList = new List<CellMsg>();

            foreach (var cell in header)
            {
                cellMsgList.Add(GetCellColumnNum(cell));
            }
            var cellMsg = cellMsgList.ToDictionary(it => it.ColName, it => it.ColNumber);


        }
        return new OkResult();
    }

    private static CellMsg GetCellColumnNum(ICell cell)
    {
        return cell.StringCellValue switch
        {
            EConsumableManage.SERIAL_NUMBER => new CellMsg(EConsumableManage.SERIAL_NUMBER, cell.Address.Column),
            EConsumableManage.NAME => new CellMsg(EConsumableManage.NAME, cell.Address.Column),
            EConsumableManage.UNIT_PRICE => new CellMsg(EConsumableManage.UNIT_PRICE, cell.Address.Column),
            EConsumableManage.GRADE => new CellMsg(EConsumableManage.GRADE, cell.Address.Column),
            EConsumableManage.QUANTITY => new CellMsg(EConsumableManage.QUANTITY, cell.Address.Column),
            EConsumableManage.REMARKS => new CellMsg(EConsumableManage.REMARKS, cell.Address.Column),
            EConsumableManage.TOTAL_PRICE => new CellMsg(EConsumableManage.TOTAL_PRICE, cell.Address.Column),
            EConsumableManage.USE => new CellMsg(EConsumableManage.USE, cell.Address.Column),
            _ => throw new Exception(),
        };
    }
}
