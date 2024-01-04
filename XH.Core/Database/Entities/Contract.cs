using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class Contract : BaseEntity
{
	public string? CorpId { get; set; }
	public string? ContractNumber { get; set; }
	public DateTime SignDate { get; set; }
	public string? OurCompany { get; set; }
	public string? OppositeCompany { get; set; }
	public string? Content { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Picture { get; set; }
	public string? ApprovalForm { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? ApprovalAnnex { get; set; }
}
