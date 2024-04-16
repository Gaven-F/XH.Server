using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Controllers;

[ApiDescriptionSettings(Order = 99)]
public class ApproveApi(ApprovedPolicyService approveService, DatabaseService dbService)
    : IDynamicApiController
{
    public void CreateApproval(EApprovedPolicy data) => approveService.Create(data);

    public IEnumerable<EApprovedPolicy> GetAllApproval(string? entityName = null)
    {
        var p = approveService.GetPolicies(entityName);
        return p.Where(it => !it.IsDeleted);
    }

    public IEnumerable<EApprovalLog> GetLog(string entityId)
    {
        var p = approveService.GetLogs(Convert.ToInt64(entityId)).Where(it => !it.IsDeleted);
        return p;
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
            .Instance.Updateable<EApprovedPolicy>()
            .Where(it => it.Id == id)
            .SetColumns(it => new EApprovedPolicy() { IsDeleted = true, UpdateTime = DateTime.Now })
            .ExecuteCommand();
    }
}
