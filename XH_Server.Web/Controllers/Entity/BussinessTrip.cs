using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;

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
