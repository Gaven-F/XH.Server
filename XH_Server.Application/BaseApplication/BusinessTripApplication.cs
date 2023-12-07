using XH_Server.Domain.Approval;
using XH_Server.Domain.DataEntities;
using XH_Server.Domain.Notification;

namespace XH_Server.Application.BaseApplication;
/// <summary>
/// 出差
/// </summary>
public class BusinessTripApplication(BasicEntityService<EBusinessTrip> eService, IApprovalService approvalService)
{
	public long CreateBussinessTrip(EBusinessTrip e)
	{
		var approvalTemplate = approvalService.GetApprovalTemplateByDataName(e.GetTableName());
		if (approvalTemplate != null)
		{
			e.ApprovalTemplateId = approvalTemplate.Id;
			e.ApprovalStatus = ApprovalStatus.Wait;
		}
		else
		{
			throw new Exception($"未找到{e.GetTableName()}的审核流程模板！");
		}

		e = eService.GetEntityById(eService.Save(e));

		approvalService.NotificationApproval(e);

		return e.Id;
	}

}
