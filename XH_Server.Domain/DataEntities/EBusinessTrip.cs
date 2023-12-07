using System.ComponentModel;
using XH_Server.Common.Attributes;
using XH_Server.Domain.Approval;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.DataEntities;

[Name("出差")]
public class EBusinessTrip : BasicEntity, IApproval
{
	public long ApprovalTemplateId { get; set; }
	public ApprovalStatus ApprovalStatus { get; set; }

	public EApprovalTemplate? Template { get; set; }

	public string SubmmiterId { get; set; } = string.Empty;

	public EStaff? Staff { get; set; }
}
