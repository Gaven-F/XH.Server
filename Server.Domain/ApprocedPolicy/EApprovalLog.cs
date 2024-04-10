using Server.Domain.Basic;

namespace Server.Domain.ApprocedPolicy;
public class EApprovalLog : BasicEntity
{
    public long EntityId { get; set; }
    public string ApproverId { get; set; } = string.Empty;
    public byte ApprovalStatus { get; set; } = 0;
    public string ApproveMsg { get; set; } = string.Empty;
    public int Index { get; set; }
}
