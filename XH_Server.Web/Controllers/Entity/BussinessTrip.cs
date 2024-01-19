using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 出差
/// </summary>
/// <param name="basicEntityService"></param>
/// <param name="approvedPolicyService"></param>
public class BussinessTrip(
		IBasicEntityService<EBussinessTrip> basicEntityService,
		ApprovedPolicyService approvedPolicyService
	) : BasicApplicationApi<EBussinessTrip, Vo.BussinessTrip>(
		basicEntityService,
		approvedPolicyService
	), IDynamicApiController
{
}
