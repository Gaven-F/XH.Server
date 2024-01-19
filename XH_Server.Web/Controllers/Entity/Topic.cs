using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 议题
/// </summary>
public class Topic : BasicApplicationApi<ETopic, Vo.Topic>, IDynamicApiController
{
}
