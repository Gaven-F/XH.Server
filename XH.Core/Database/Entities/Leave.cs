using SqlSugar;
using System;
using System.Collections.Generic;

namespace XH.Core.DataBase.Entities;

public class Leave : BaseEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string? LeaveType { get; set; }
	public string? AnnualLeave { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public string? SumTime { get; set; }
	public string? ReasonLeave { get; set; }
	[SugarColumn(IsJson = true)]
	public List<string>? Annex { get; set; }
	public string StartDateSuffix { get; set; } = "上午";
	public string EndDateSuffix { get; set; } = "上午";
}
