using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class IssueReceipts : BaseEntity
{
	public string? CorpId { get; set; }
	public string? InvoiceType { get; set; }
	public string? ApplicatDepart { get; set; }
	public string? ReasonReceipt { get; set; }
	public string? ReceiptAmount { get; set; }
	public string? ReceiptDate { get; set; }
	public string? ReceiptCompany { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Annex { get; set; }
}
