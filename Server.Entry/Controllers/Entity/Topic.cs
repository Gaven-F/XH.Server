using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 议题
/// </summary>
public class Topic : BasicApplicationApi<ETopic>, IDynamicApiController
{
    [NonAction]
    public override Results<Ok<List<Tuple<ETopic, EApprovalLog>>>, BadRequest<string>> GetData()
    {
        return base.GetData();
    }

    public Results<
        Ok<List<Tuple<ETopic, IEnumerable<EApprovalLog>>>>,
        BadRequest<string>
    > GetDataWithLogs()
    {
        try
        {
            var data = BasicEntityService.GetEntities();
            List<Tuple<ETopic, IEnumerable<EApprovalLog>>> res = new(data.Count());
            foreach (var entity in data)
            {
                var log = ApprovedPolicyService.GetLogs(entity.Id).Adapt<List<EApprovalLog>>();
                res.Add(new(entity, log));
            }
            return TypedResults.Ok(res);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}
