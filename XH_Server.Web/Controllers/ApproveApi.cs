using Microsoft.AspNetCore.Mvc;
using XH_Server.Domain.Approve;

namespace XH_Server.Web.Controllers;

public class ApproveApi(IApproveService approveService) : ControllerBase
{
	public string CreateApprovalTemplate(EApprovalTemplate template)
	{
		return approveService.CreateApprovalTemplate(template).ToString();
	}

	public string CreateApprocalNode(EApprovalNode node, long templateId)
	{
		return approveService.CreateApprocalNode(node, templateId).ToString();

	}
}
