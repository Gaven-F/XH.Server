using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approve;
public class EApprovalHistory : BasicEntity
{
	public long ApprovalEntityId { get; set; }
	public long NodeId { get; set; }
	public ApprovalStatus Status { get; set; }
	public string Msg { get; set; } = string.Empty;
}
