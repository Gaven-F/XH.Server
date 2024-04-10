using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;


namespace Server.Web.Controllers.Entity;

/// <summary>
/// 会议室
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class MeetingRoomApplication(
    IBasicEntityService<EMeetingRoomApplication> bes,
    ApprovedPolicyService aps)
    : BasicApplicationApi<EMeetingRoomApplication, Vo.MeetingRoomApplication>(
        bes, aps)
    , IDynamicApiController
{

}
