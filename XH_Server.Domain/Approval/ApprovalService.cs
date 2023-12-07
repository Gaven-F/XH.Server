using XH_Server.Core.Database;
using XH_Server.Domain.Basic;
using XH_Server.Domain.DataEntities;
using XH_Server.Domain.Notification;

namespace XH_Server.Domain.Approval;

public interface IApprovalService
{
	public IEnumerable<EApprovalTemplate> GetAllApprovalTemplate();
	public EApprovalTemplate GetApprovalTemplateByDataName(string dataName);
	public void CreateApprovalTemplate(EApprovalTemplate template);
	public void GetNextApprovalNode(long currentNodeId);

	public void ApprovalData<T>(T data, long nodeId, string msg, ApprovalStatus status = ApprovalStatus.Pass) where T : IApproval;
	public (ApprovalStatus status, string ResultMsg) GetApprovalResult<T>(T data) where T : IApproval;
	public void NotificationApproval<T>(T data) where T : IApproval;
}
// TODO 实现接口
public class ApprovalService()
{

}