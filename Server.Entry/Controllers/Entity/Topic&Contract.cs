using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;

namespace Server.Web.Controllers.Entity;

public class Topic_Contract : BasicApplicationApi<ET_C, ET_C>, IDynamicApiController
{
    public override Results<Ok<string>, BadRequest<string>> Add(ET_C entity)
    {
        try
        {
            var id = BasicEntityService.Create(entity);

            //ApprovedPolicyService.CreateApproveBasicLog(entity);
            //添加审核人员
            //ApprovedPolicyService
            var logs = new List<EApprovalLog>
            {
                new()
                {
                    EntityId = id,
                    ApproverId = "01052133605426110345",
                    Topic = true,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "01052133605426110345",
                    Topic = true,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "01052133605426110345",
                    Topic = false,
                }
            };

            Db.Instance.Insertable(logs).ExecuteReturnSnowflakeIdList();

            var log = ApprovedPolicyService.GetCurrentApprovalLog(id);
            if (log != null)
            {
                DingtalkUtils.SendMsg([log.ApproverId.ToString()], $"有一个待审核的消息！\r\n数据ID：{entity.Id}");
            }

            return TypedResults.Ok(id.ToString());
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public Results<Ok<List<Tuple<object, EApprovalLog>>>, BadRequest<string>> GetDataDemo()
    {
        try
        {
            var data = BasicEntityService.GetEntities();
            var ps = typeof(ET_C).GetProperties();
            var filterps = ps.Where(p => p.Name.StartsWith("T", StringComparison.OrdinalIgnoreCase) || p.Name.Equals("id", StringComparison.OrdinalIgnoreCase));

            var demo = data
                .Select(d => filterps.ToDictionary(p =>
                    p.Name, p => p.GetValue(d)?.ToString() ?? ""))
                .ToList();

            List<Tuple<object, EApprovalLog>> res = new(data.Count());
            foreach (var entity in demo)
            {
                var log = ApprovedPolicyService.GetCurrentApprovalLog(Convert.ToInt64(entity["Id"])).Adapt<Vo.ApproLog>();
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