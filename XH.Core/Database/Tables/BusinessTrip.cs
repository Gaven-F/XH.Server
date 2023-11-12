using SqlSugar;
using System;
using System.Collections.Generic;
using XH.Core.DataBase.Tables;

namespace XH.Core.Database.Tables;
public class BusinessTrip : BaseTable
{
    [SugarColumn(ColumnDescription = "申请人Id号")]
    public string CorpId { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "出差类型")]
    public string Type { get; set; } = "NONE";

    [SugarColumn(ColumnDescription = "目的地")]
    public string Destination { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "出发地")]
    public string Departure { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "交通工具")]
    public string Vehicle { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "出差成员", ColumnDataType = "JSON")]
    public string Members { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "开始时间")]
    public DateTimeOffset StartTime { get; set; }

    [SugarColumn(ColumnDescription = "结束时间")]
    public DateTimeOffset EndTime { get; set; }

    [SugarColumn(ColumnDescription = "出差事由", ColumnDataType = "varchar(1024)")]
    public string Description { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "相关附件", ColumnDataType = "JSON")]
    public string Annex { get; set; } = string.Empty;

    [SugarColumn(ColumnDescription = "审核情况")]
    public int status { get; set; }
}

public class FBusinessTrip
{
    public string CorpId { get; set; } = string.Empty;

    public string Type { get; set; } = "NONE";

    public string Destination { get; set; } = string.Empty;

    public string Departure { get; set; } = string.Empty;

    public string Vehicle { get; set; } = string.Empty;

    public List<string> Members { get; set; } = new List<string>();
 
    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset EndTime { get; set; }

    public string Description { get; set; } = string.Empty;

    public List<string> Annex { get; set; } = new List<string>();
}
