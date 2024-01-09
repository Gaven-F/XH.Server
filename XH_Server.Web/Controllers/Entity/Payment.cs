using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 付款
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class Payment(
	IBasicEntityService<EPayment> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EPayment>(
		bes, aps)
	, IDynamicApiController
{

}
