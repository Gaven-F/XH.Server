using XH_Server.Domain.DataEntities;

namespace XH_Server.Domain.Approval;
public interface IApproval
{
	public long ApprovalTemplateId { get; set; }
	public ApprovalStatus ApprovalStatus { get; set; }
	public string SubmmiterId { get; set; }

	[Navigate(NavigateType.OneToOne, nameof(ApprovalTemplateId))]
	public EApprovalTemplate? Template { get; set; }
	[Navigate(NavigateType.OneToOne, nameof(SubmmiterId), nameof(EStaff.UserId))]
	public EStaff? Staff { get; set; }
}
