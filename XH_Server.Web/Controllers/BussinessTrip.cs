using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;

namespace XH_Server.Web.Controllers;

public class BussinessTrip(
		IBasicEntityService<EBussinessTrip> basicEntityService,
		ApprovedPolicyService approvedPolicyService
	) : BasicApplicationApi<EBussinessTrip>(
		basicEntityService,
		approvedPolicyService
	), IDynamicApiController
{

}

public class EDemo : BasicEntity
{

}

public class Demo(
	IBasicEntityService<EDemo> BES,
	ApprovedPolicyService APS)
	: BasicApplicationApi<EDemo>(
		BES,
		APS)
	, IDynamicApiController
{
}
