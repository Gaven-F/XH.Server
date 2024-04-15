using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 报销
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class Reimbursement(IBasicEntityService<EReimbursement> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EReimbursement, Vo.Reimbursement>(bes, aps),
        IDynamicApiController { }
