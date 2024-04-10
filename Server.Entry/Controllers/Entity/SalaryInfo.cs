using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;

namespace Server.Web.Controllers.Entity;

public class SalaryInfo : BasicApplicationApi<ESalaryInfo, ESalaryInfo>, IDynamicApiController
{

}
