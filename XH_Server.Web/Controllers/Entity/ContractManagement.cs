using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 合同管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ContractManagement(
	IBasicEntityService<EContractManagement> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EContractManagement>(
		bes, aps)
	, IDynamicApiController
{

}
