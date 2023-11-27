using SqlSugar;
using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class Seal : BaseEntity
{
	public string? CorpId { get; set; }
	public string? Applicatdepart { get; set; }
	public string? Handledby { get; set; }
	public DateTimeOffset Date { get; set; }
	public string? ReasonBorrow { get; set; }
	public DateTimeOffset StartDate { get; set; }
	public DateTimeOffset EndDate { get; set; }
	[SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Sealtype { get; set; }
}
