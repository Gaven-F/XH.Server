using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Entities.Dto;
using Server.Core.Database;
using Server.Domain.ApprocedPolicy;

namespace Server.Web.Controllers;

[ApiDescriptionSettings(Order = 99)]
public class ApproveApi(ApprovedPolicyService approveService, DatabaseService dbService) : IDynamicApiController
{
    public void CreateApproval(EApprovedPolicy data) { approveService.Create(data); }

    public IEnumerable<Vo.ApprovedPolicy> GetAllApproval(string? entityName = null)
    {
        var p = approveService.GetPolicies(entityName);
        return p.Adapt<IList<Vo.ApprovedPolicy>>().Where(it => !it.IsDeleted);
    }

    public IEnumerable<Vo.ApproLog> GetLog(string entityId)
    {
        var p = approveService.GetLogs(Convert.ToInt64(entityId));
        return p.Adapt<List<Vo.ApproLog>>();
    }

    /// <summary>
    /// 删除策略
    /// </summary>
    /// <param name="policyId"></param>
    /// <returns></returns>
    public int DeleteApproval(string policyId)
    {
        var id = Convert.ToInt64(policyId);
        return dbService
            .Instance
            .Updateable<EApprovedPolicy>()
            .Where(it => it.Id == id)
            .SetColumns(it => new EApprovedPolicy() { IsDeleted = true, UpdateTime = DateTime.Now })
            .ExecuteCommand();
    }
}
