using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 用印
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class UseSeal(IBasicEntityService<EUseSeal> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EUseSeal, Vo.UseSeal>(bes, aps),
        IDynamicApiController { }
