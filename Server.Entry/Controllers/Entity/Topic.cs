using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprovedPolicy;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 议题
/// </summary>
public class Topic : BasicApplicationApi<ETopic, Vo.Topic>, IDynamicApiController
{
    [NonAction]
    public override Results<Ok<List<Tuple<Vo.Topic, EApprovalLog>>>, BadRequest<string>> GetData()
    {
        return base.GetData();
    }

    public Results<Ok<List<Tuple<Vo.Topic, IEnumerable<EApprovalLog>>>>, BadRequest<string>> GetDataWithLogs()
    {
        try
        {
            var data = BasicEntityService.GetEntities();
            List<Tuple<Vo.Topic, IEnumerable<EApprovalLog>>> res = new(data.Count());
            foreach (var entity in data)
            {
                var log = ApprovedPolicyService
                    .GetLogs(entity.Id)
                    .Adapt<List<Vo.ApproLog>>();
                res.Add(new(entity.Adapt<Vo.Topic>(), log));
            }
            return TypedResults.Ok(res);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}

