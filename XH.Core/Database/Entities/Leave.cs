using SqlSugar;
using System;
using System.Collections.Generic;

namespace XH.Core.DataBase.Entities;

public class Leave : BaseEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string? LeaveType { get; set; }
    public string? AnnualLeave { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string? SumTime { get; set; }
    public string? ReasonLeave { get; set; }
    [SugarColumn(IsJson = true)]
    public List<string>? Annex { get; set; }
}
