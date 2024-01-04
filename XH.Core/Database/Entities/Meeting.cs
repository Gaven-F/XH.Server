using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class Meeting : BaseEntity
{
	public string? CorpId { get; set; }
	public string? Affiliatedcompany { get; set; }
	public DateTime? ApplicationTime { get; set; }
	public DateTime? Usagetime { get; set; }
	public string? Reasonborrow { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Annex { get; set; }
}
