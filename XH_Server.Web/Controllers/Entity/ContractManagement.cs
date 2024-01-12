using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;


namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 合同管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ContractManagement(
	IBasicEntityService<EContractManagement> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EContractManagement, Dtos.ContractManagement>(
		bes, aps)
	, IDynamicApiController
{

}
