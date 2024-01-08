using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;

namespace XH_Server.Application;

public class BasicApplicationApi<T>(
	IBasicEntityService<T> basicEntityService,
	ApprovedPolicyService approvedPolicyService
	)
	where T : BasicEntity
{
	public virtual long Add(T entity)
	{
		var id = basicEntityService.Create(entity);
		approvedPolicyService.CreateApproveBasicLog(entity);
		var log = approvedPolicyService.GetCurrentApprovalLog(id);

		DingtalkUtils.SendMsg([log.ApproverId.ToString()], "有一个待审核的消息！");

		return id;
	}

	public virtual bool Approve(long logId, byte status, string msg = "无")
	{
		var eId = approvedPolicyService.GetLogById(logId).EntityId;
		approvedPolicyService.Approve(logId, status, msg);
		var cLog = approvedPolicyService.GetCurrentApprovalLog(eId);

		if (cLog == null)
		{
			var e = basicEntityService.GetEntityById(eId);
			DingtalkUtils.SendMsg(approvedPolicyService.GetPolicy<T>(eId).CopyIds.Split(','), $"""
				抄送信息：
				{e.CreateTime.ToLongDateString()}
				""");

			return true;
		}

		DingtalkUtils.SendMsg([cLog.ApproverId.ToString()], "有一个待审核的消息！");

		return true;

	}

	public virtual int Delete(long eId)
	{
		return basicEntityService.Delete(eId);
	}

	public IEnumerable<(T, EApprovalLog)> GetData()
	{
		var data = basicEntityService.GetEntities();
		List<(T, EApprovalLog)> res = new(data.Count());
		foreach (var entity in data)
		{
			var log = approvedPolicyService.GetCurrentApprovalLog(entity.Id);
			res.Add((entity, log));
		}
		return res;
	}

	public virtual (T, EApprovalLog) GetDataById(long id)
	{
		return (basicEntityService.GetEntityById(id), approvedPolicyService.GetCurrentApprovalLog(id));
	}
}

