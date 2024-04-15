using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 项目管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ProjectManagement(
    IBasicEntityService<EProjectManagement> bes,
    ApprovedPolicyService aps
)
    : BasicApplicationApi<EProjectManagement, Vo.ProjectManagement>(bes, aps),
        IDynamicApiController { }
