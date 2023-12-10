using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approve;

public class ApprovedEntity : BasicEntity
{
	public long ApprovalTemplateId { get; set; }
	public ApprovalStatus ApprovalType { get; set; }
	public long TotalApprovalMin { get; set; }
}
