using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EReimbursement : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string TypeExpense { get; set; } = string.Empty;
	public string Amount { get; set; } = string.Empty;
	public string FeeBreakdown { get; set; } = string.Empty;
	public string AssociateOtherId { get; set; } = string.Empty;
	public string Annex { get; set; } = string.Empty;
	public string Remark { get; set; } = string.Empty;
	public string LargeAmount { get; set; } = string.Empty;
}

