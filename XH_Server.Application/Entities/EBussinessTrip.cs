using XH_Server.Domain.Approve;
using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;
public class EBussinessTrip : BasicEntity, IApprovedEntity
{
	public long ApprovalTemplateId { get; set; }
	public ApprovalStatus ApprovalType { get; set; }
	public long TotalApprovalMin { get; set; }
}
