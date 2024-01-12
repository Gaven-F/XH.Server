using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;


namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 订单
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class OrderManagement(
	IBasicEntityService<EOrderManagement> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EOrderManagement, Dtos.OrderManagement>(
		bes, aps)
	, IDynamicApiController
{

}
