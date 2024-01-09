using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using static XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 项目管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ProjectManagement(
	IBasicEntityService<EProjectManagement> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EProjectManagement>(
		bes, aps)
	, IDynamicApiController
{

}
