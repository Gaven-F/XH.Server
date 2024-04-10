using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;

namespace Server.Web.Controllers.Entity;

public class Topic_Contract : BasicApplicationApi<ET_C, ET_C>, IDynamicApiController
{
}
