using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class MeetingLog : BaseEntity
{
	public string? CorpId { get; set; }
	public string? MeetingNumber { get; set; }
	public string? SummaryName { get; set; }
	public DateTime MeetingDate { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? MeetingAttachments { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? MeetingMinutes { get; set; }

	public string? Notes { get; set; }
	public string? State { get; set; }

}
