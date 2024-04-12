using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 议题
/// </summary>
public class Topic : BasicApplicationApi<ETopic, Vo.Topic>, IDynamicApiController
{
}