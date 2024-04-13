using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 出差
/// </summary>
/// <param name="basicEntityService"></param>
/// <param name="approvedPolicyService"></param>
public class BussinessTrip(
    IBasicEntityService<EBusinessTrip> basicEntityService,
    ApprovedPolicyService approvedPolicyService
)
    : BasicApplicationApi<EBusinessTrip, Vo.BussinessTrip>(
        basicEntityService,
        approvedPolicyService
    ),
        IDynamicApiController { }
