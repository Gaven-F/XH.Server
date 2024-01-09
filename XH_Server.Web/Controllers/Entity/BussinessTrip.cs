using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;

public class BussinessTrip(
        IBasicEntityService<EBussinessTrip> basicEntityService,
        ApprovedPolicyService approvedPolicyService
    ) : BasicApplicationApi<EBussinessTrip>(
        basicEntityService,
        approvedPolicyService
    ), IDynamicApiController
{

}
