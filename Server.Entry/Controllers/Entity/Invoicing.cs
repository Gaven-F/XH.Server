using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 发票
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class Invoicing(IBasicEntityService<EInvoicing> bes, ApprovedPolicyService aps)
    : BasicApplicationApi<EInvoicing, Vo.Invoicing>(bes, aps),
        IDynamicApiController { }
