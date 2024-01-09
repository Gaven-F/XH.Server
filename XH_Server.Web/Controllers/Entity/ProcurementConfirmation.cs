using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 采购确认
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ProcurementConfirmation(
	IBasicEntityService<EProcurementConfirmation> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EProcurementConfirmation>(
		bes, aps)
	, IDynamicApiController
{

}
