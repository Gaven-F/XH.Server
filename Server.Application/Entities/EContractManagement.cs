using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EContractManagement : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string ContractNumber { get; set; } = string.Empty;
    public DateTime SignDate { get; set; }
    public string OurCompany { get; set; } = string.Empty;
    public string OppositeCompany { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public string AssociateOtherId { get; set; } = string.Empty;
    public string ApprovalAnnex { get; set; } = string.Empty;
}

