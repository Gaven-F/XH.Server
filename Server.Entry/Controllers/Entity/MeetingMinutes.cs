using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 会议纪要
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class MeetingMinutes(IBasicEntityService<EMeetingMinutes> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EMeetingMinutes, Vo.MeetingMinutes>(bes, aps),
        IDynamicApiController { }
