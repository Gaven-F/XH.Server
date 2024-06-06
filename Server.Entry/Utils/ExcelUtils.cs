using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Server.Entry.Utils;

public class ExcelUtils
{
    /// <summary>
    /// Excel导入成DataTble
    /// </summary>
    /// <returns></returns>
    public static DataTable? ExcelToTable(Stream file, string name)
    {
        var dt = new DataTable();
        IWorkbook? workbook;
        var fileExt = Path.GetExtension(name).ToLower();
        if (fileExt == ".xlsx")
        {
            workbook = new XSSFWorkbook(file);
        }
        else if (fileExt == ".xls")
        {
            workbook = new HSSFWorkbook(file);
        }
        else
        {
            workbook = null;
        }
        if (workbook == null)
        {
            return null;
        }
        ISheet sheet = workbook.GetSheetAt(0);

        //表头
        var header = sheet.GetRow(sheet.FirstRowNum);
        var columns = new List<int>();
        for (int i = 0; i < header.LastCellNum; i++)
        {
            var obj = GetValueType(header.GetCell(i));
            if (obj == null || obj.ToString() == string.Empty)
            {
                dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
            }
            else
                dt.Columns.Add(new DataColumn(obj.ToString()));
            columns.Add(i);
        }
        //数据
        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
        {
            var dr = dt.NewRow();
            bool hasValue = false;
            foreach (int j in columns)
            {
                dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                if (dr[j] != null && dr[j].ToString() != string.Empty)
                {
                    hasValue = true;
                }
            }
            if (hasValue)
            {
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    /// <summary>
    /// 获取单元格类型
    /// </summary>
    /// <param name="cell">目标单元格</param>
    /// <returns></returns>
    private static object? GetValueType(ICell cell)
    {
        if (cell == null)
            return null;
        return cell.CellType switch
        {
            CellType.Blank => null,
            CellType.Boolean => cell.BooleanCellValue,
            CellType.Numeric => cell.NumericCellValue,
            CellType.String => cell.StringCellValue,
            CellType.Error => cell.ErrorCellValue,
            _ => cell.NumericCellValue,
        };
    }
}
