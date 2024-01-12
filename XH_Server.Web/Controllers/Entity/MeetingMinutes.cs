using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;


namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 会议纪要
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class MeetingMinutes(
	IBasicEntityService<EMeetingMinutes> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EMeetingMinutes, Dtos.MeetingMinutes>(
		bes, aps)
	, IDynamicApiController
{

}
