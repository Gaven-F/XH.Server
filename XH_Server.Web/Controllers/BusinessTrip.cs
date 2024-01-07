using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Domain.Approve;

namespace XH_Server.Web.Controllers;

public class BusinessTrip(IBasicApplicationService<EBussinessTrip> app) : ControllerBase
{
	public IEnumerable<EBussinessTrip> GetData()
	{
		return app.GetData();
	}

	public long PostData(EBussinessTrip e)
	{
		return app.Add(e);
	}

	[HttpGet]
	public bool Approval(long eId, long nodeId, string msg, ApprovalStatus status)
	{
		return app.Approve(eId, nodeId, msg, status);
	}
}
