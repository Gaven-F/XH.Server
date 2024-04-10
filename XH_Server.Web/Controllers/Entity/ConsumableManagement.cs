using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 耗材
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ConsumableManagement(
    IBasicEntityService<EConsumableManagement> bes,
    ApprovedPolicyService aps)
    : BasicApplicationApi<EConsumableManagement, Vo.ConsumableManagement>(
        bes, aps)
    , IDynamicApiController
{

}
