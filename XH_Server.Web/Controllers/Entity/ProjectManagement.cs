﻿using Furion.DynamicApiController;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;


namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 项目管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ProjectManagement(
	IBasicEntityService<EProjectManagement> bes,
	ApprovedPolicyService aps)
	: BasicApplicationApi<EProjectManagement, Vo.ProjectManagement>(
		bes, aps)
	, IDynamicApiController
{

}
