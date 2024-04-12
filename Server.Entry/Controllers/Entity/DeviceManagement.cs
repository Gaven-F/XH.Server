using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 设备管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class DeviceManagement(
    IBasicEntityService<EDeviceManagement> bes,
    ApprovedPolicyService aps)
    : BasicApplicationApi<EDeviceManagement, Vo.DeviceManagement>(
        bes, aps)
    , IDynamicApiController
{
}