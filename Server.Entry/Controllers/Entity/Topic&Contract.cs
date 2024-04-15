using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Application;
using Server.Application.Entities;
using Server.Domain.ApprovedPolicy;

namespace Server.Web.Controllers.Entity;

public class Topic_Contract : BasicApplicationApi<ET_C, ET_C>, IDynamicApiController
{
    public override Results<Ok<string>, BadRequest<string>> Add(ET_C entity)
    {
        try
        {
            var id = BasicEntityService.Create(entity);

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
                DingTalkUtils.SendMsg(
                    [log.ApproverId.ToString()],
                    $"有一个待审核的消息！\r\n数据ID：{entity.Id}"
                );
            }

            return TypedResults.Ok(id.ToString());
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}
