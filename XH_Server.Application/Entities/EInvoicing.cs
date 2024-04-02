using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EInvoicing : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string InvoicingType { get; set; } = string.Empty;
	public string ApplicatDepart { get; set; } = string.Empty;
	public string ReasonInvoic { get; set; } = string.Empty;
	public string BillType { get; set; } = string.Empty;
	public string InvoiceAmount { get; set; } = string.Empty;
	public DateTime InvoicDate { get; set; }
	public string InvoicCompany { get; set; } = string.Empty;
	public string Annex { get; set; } = string.Empty;
	public string LargeAmount { get; set; } = string.Empty;
	public string ContractNumber { get; set; } = string.Empty;
}

