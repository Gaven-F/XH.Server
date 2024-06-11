using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Server.Web.Controllers.Entity;

public class Topic_Contract : BasicApplicationApi<ET_C>, IDynamicApiController
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
                    ApproverId = "01053105311926842883",
                    Topic = true,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "manager4797",
                    Topic = true,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "22096954321151228",
                    Topic = true,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "153640044327257189",
                    Topic = true,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "01053105311926842883",
                    Topic = false,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "manager4797",
                    Topic = false
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "303356575132379370",
                    Topic = false,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "1743462321848691",
                    Topic = false,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "22096954321151228",
                    Topic = false,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "153640044327257189",
                    Topic = false,
                },
                new()
                {
                    EntityId = id,
                    ApproverId = "19302407641212774",
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
