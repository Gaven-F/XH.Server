using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 会议室
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class MeetingRoomApplication(
	IBasicEntityService<EMeetingRoomApplication> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EMeetingRoomApplication>(
		bes, aps)
	, IDynamicApiController
{

}
