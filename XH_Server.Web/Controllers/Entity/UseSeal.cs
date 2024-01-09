using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;


/// <summary>
/// 用印
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class UseSeal(
	IBasicEntityService<EUseSeal> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EUseSeal>(
		bes, aps)
	, IDynamicApiController
{

}
