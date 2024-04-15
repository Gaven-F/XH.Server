using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 采购申请
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class PurchaseRequest(IBasicEntityService<EPurchaseRequest> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EPurchaseRequest, Vo.PurchaseRequest>(bes, aps),
        IDynamicApiController { }
