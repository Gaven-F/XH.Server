using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 采购确认
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ProcurementConfirmation(
    IBasicEntityService<EProcurementConfirmation> bes,
    ApprovedPolicyService aps)
    : BasicApplicationApi<EProcurementConfirmation, Vo.ProcurementConfirmation>(
        bes, aps)
    , IDynamicApiController
{
}