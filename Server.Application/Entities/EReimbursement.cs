using Server.Domain.Basic;

namespace Server.Application.Entities;

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
