using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 付款
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class Payment(IBasicEntityService<EPayment> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EPayment, Vo.Payment>(bes, aps),
        IDynamicApiController { }
