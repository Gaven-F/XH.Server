using XH_Server.Domain.Approval;
using XH_Server.Domain.Basic;
using XH_Server.Domain.DataEntities;

namespace XH_Server.Application.BasicApplication;

public class BasicApplicationService<T>(BasicEntityService<T> eService, IApprovalService approvalService) where T : BasicEntity, IApproval
{
	public long CreateDataWithApproval(T e)
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

	public long ApprovalData(long id)
	{
		var e = 
	}

}
