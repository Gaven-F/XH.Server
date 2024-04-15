using Furion.DynamicApiController;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 合同管理
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ContractManagement(
    IBasicEntityService<EContractManagement> bes,
    ApprovedPolicyService aps
)
    : BasicApplicationApi<EContractManagement, Vo.ContractManagement>(bes, aps),
        IDynamicApiController { }
