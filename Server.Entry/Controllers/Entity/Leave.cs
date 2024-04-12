using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 请假
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class Leave(IBasicEntityService<ELeave> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<ELeave, Vo.Leave>(bes, aps),
        IDynamicApiController { }
