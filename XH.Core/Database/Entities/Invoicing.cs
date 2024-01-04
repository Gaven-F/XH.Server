using System.Collections.Generic;


namespace XH.Core.DataBase.Entities;

public class Invoicing : BaseEntity
{
	public string? CorpId { get; set; }
	public string? InvoicingType { get; set; }
	public string? ApplicatDepart { get; set; }
	public string? ReasonInvoic { get; set; }
	public string? BillType { get; set; }
	public string? InvoiceAmount { get; set; }
	public string? InvoicDate { get; set; }
	public string? InvoicCompany { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Annex { get; set; }
}
