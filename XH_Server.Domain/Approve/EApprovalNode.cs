using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approve;
public class EApprovalNode : BasicEntity
{
	public long ApprovalTemplateId { get; set; }
	public ApprovalNodeType NodeType { get; set; } = ApprovalNodeType.Approve;
	public long NextNodeId { get; set; }
	public long ApprovalerId { get; set; }
}
