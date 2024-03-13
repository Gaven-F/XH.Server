using Mapster;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;

namespace XH_Server.Web.Controllers;

[ApiDescriptionSettings(Order = 99)]
public class ApproveApi(ApprovedPolicyService approveService) : ControllerBase
{
	public void CreateApproval(EApprovedPolicy data)
	{
		approveService.Create(data);
	}

	public IEnumerable<Vo.ApprovedPolicy> GetAllApproval(string? entityName = null)
	{
		var p = approveService.GetPolicies(entityName);
		return p.Adapt<IList<Vo.ApprovedPolicy>>();
	}


	public IEnumerable<Vo.ApproLog> GetLog(string entityId)
	{
		var p = approveService.GetLogs(Convert.ToInt64(entityId));
		return p.Adapt<List<Vo.ApproLog>>();
	}
}
