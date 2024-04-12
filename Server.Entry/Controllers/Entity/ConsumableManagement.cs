using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

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