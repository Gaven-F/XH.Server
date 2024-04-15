using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 收据
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class IssueReceipts(IBasicEntityService<EIssueReceipts> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EIssueReceipts, Vo.IssueReceipts>(bes, aps),
        IDynamicApiController { }
