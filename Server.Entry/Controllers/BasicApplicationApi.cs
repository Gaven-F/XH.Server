using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Entry.Utils;

namespace Server.Application;

public class BasicApplicationApi<T>
    where T : BasicEntity
{
    [FromServices]
    public Utils.BaseFunc DingTalkUtils { get; set; }

    [FromServices]
    public IBasicEntityService<T> BasicEntityService { get; set; }

    [FromServices]
    public ApprovedPolicyService ApprovedPolicyService { get; set; }

    [FromServices]
    public DatabaseService Db { get; set; }

#pragma warning disable 8618

    public virtual Results<Ok<string>, BadRequest<string>> Add(T entity)
    {
        try
        {
            var id = BasicEntityService.Create(entity);
            ApprovedPolicyService.CreateApproveBasicLog(entity);
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

    public virtual Results<Ok<int>, BadRequest<string>> Delete(long eId)
    {
        try
        {
            return TypedResults.Ok(BasicEntityService.Delete(eId));
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public virtual Results<Ok<List<Tuple<T, EApprovalLog>>>, BadRequest<string>> GetData()
    {
        try
        {
            var data = BasicEntityService.GetEntities();
            List<Tuple<T, EApprovalLog>> res = new(data.Count());
            foreach (var entity in data)
            {
                var log = ApprovedPolicyService.GetCurrentApprovalLog(entity.Id);
                res.Add(new(entity, log));
            }
            return TypedResults.Ok(res);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public virtual Results<Ok<Tuple<T, EApprovalLog>>, BadRequest<string>> GetDataById(long id)
    {
        try
        {
            return TypedResults.Ok(
                new Tuple<T, EApprovalLog>(
                    BasicEntityService.GetEntityById(id),
                    ApprovedPolicyService.GetCurrentApprovalLog(id)
                )
            );
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public BasicApplicationApi(
        IBasicEntityService<T> basicEntityService,
        ApprovedPolicyService approvedPolicyService
    )
    {
        BasicEntityService = basicEntityService;
        ApprovedPolicyService = approvedPolicyService;
    }

    public BasicApplicationApi() { }

#pragma warning restore
    public virtual Results<Ok<bool>, BadRequest<string>> Approve(
        long logId,
        byte status,
        string msg = "无"
    )
    {
        try
        {
            var eId = ApprovedPolicyService.GetLogById(logId).EntityId;
            ApprovedPolicyService.Approve(logId, status, msg);
            var cLog = ApprovedPolicyService.GetCurrentApprovalLog(eId);

            if (cLog == null)
            {
                var e = BasicEntityService.GetEntityById(eId);

                var formatFunc = CopyMsgFormat.FormatMapper[e.GetType().Name];
                if (formatFunc != null)
                {
                    DingTalkUtils.SendMsg(
                        ApprovedPolicyService.GetPolicy<T>(eId).CopyIds.Split(','),
                        formatFunc(e)
                    );
                }
                else
                {
                    DingTalkUtils.SendMsg(
                        ApprovedPolicyService.GetPolicy<T>(eId).CopyIds.Split(','),
                        $"""
                        您有一条抄送消息：
                        {e.CreateTime.ToLongDateString()}
                        """
                    );
                }

                return TypedResults.Ok(true);
            }

            DingTalkUtils.SendMsg([cLog.ApproverId.ToString()], "有一个待审核的消息！");

            return TypedResults.Ok(true);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}
