using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EContractManagement : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string ContractNumber { get; set; } = string.Empty;
	public DateTime SignDate { get; set; }
	public string OurCompany { get; set; } = string.Empty;
	public string OppositeCompany { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public string Picture { get; set; } = string.Empty;
	public string ApprovalForm { get; set; } = string.Empty;
	public string ApprovalAnnex { get; set; } = string.Empty;
}

