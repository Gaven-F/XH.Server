using SqlSugar;
using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;
using XH.Core.Interfaces.AsTools;

namespace XH.Core.Database.Entities;

public class BusinessTrip : BaseEntity, INeedApprove
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

	[SugarColumn(ColumnDescription = "出差成员", ColumnDataType = "JSON", IsJson = true)]
	public List<string> Members { get; set; } = new List<string>();

	[SugarColumn(ColumnDescription = "开始时间")]
	public DateTime StartTime { get; set; }

	[SugarColumn(ColumnDescription = "结束时间")]
	public DateTime EndTime { get; set; }

	[SugarColumn(ColumnDescription = "出差事由", ColumnDataType = "varchar(1024)")]
	public string Description { get; set; } = string.Empty;

	[SugarColumn(ColumnDescription = "相关附件", ColumnDataType = "JSON", IsJson = true)]
	public List<string> Annex { get; set; } = new List<string>();

	[SugarColumn(ColumnDescription = "审核流程起点")]
	public long StartApprovalStep { get; set; }

	public TimeSpan SumTripTime => StartTime - EndTime;


	public string StartDateSuffix { get; set; } = "上午";
	public string EndDateSuffix { get; set; } = "上午";

}

public class FBusinessTrip
{
	public string CorpId { get; set; } = string.Empty;

	public string Type { get; set; } = "NONE";

	public string Destination { get; set; } = string.Empty;

	public string Departure { get; set; } = string.Empty;

	public string Vehicle { get; set; } = string.Empty;

	public List<string> Members { get; set; } = new List<string>();

	public DateTime StartTime { get; set; }

	public DateTime EndTime { get; set; }

	public string Description { get; set; } = string.Empty;

	public List<string> Annex { get; set; } = new List<string>();
	public string StartDateSuffix { get; set; } = "上午";
	public string EndDateSuffix { get; set; } = "上午";
}
