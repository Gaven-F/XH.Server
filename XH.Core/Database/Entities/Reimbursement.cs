using System.Collections.Generic;


namespace XH.Core.DataBase.Entities;

public class Reimbursement : BaseEntity
{
	public string? CorpId { get; set; }
	public string? TypeExpense { get; set; }
	public string? Amount { get; set; }
	public string? FeeBreakdown { get; set; }
	public string? ExpenseRelated { get; set; }
	public string? Remark { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Annex { get; set; }
}
