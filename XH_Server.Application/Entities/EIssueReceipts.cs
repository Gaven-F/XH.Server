using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EIssueReceipts : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string InvoiceType { get; set; } = string.Empty;
	public string ApplicatDepart { get; set; } = string.Empty;
	public string ReasonReceipt { get; set; } = string.Empty;
	public string ReceiptAmount { get; set; } = string.Empty;
	public DateTime ReceiptDate { get; set; }
	public string ReceiptCompany { get; set; } = string.Empty;
	public string Annex { get; set; } = string.Empty;
	public string LargeAmount { get; set; } = string.Empty;
}

