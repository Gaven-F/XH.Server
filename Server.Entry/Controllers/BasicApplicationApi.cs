using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Entities.Dto;
using Server.Core.Database;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;
using Server.Web;

namespace Server.Application;

public class BasicApplicationApi<T, VoT>
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
                DingTalkUtils.SendMsg(
                    ApprovedPolicyService.GetPolicy<T>(eId).CopyIds.Split(','),
                    $"""
                    抄送信息：
                    {e.CreateTime.ToLongDateString()}
                    """
                );

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

    public virtual Results<Ok<List<Tuple<VoT, EApprovalLog>>>, BadRequest<string>> GetData()
    {
        try
        {
            var data = BasicEntityService.GetEntities();
            List<Tuple<VoT, EApprovalLog>> res = new(data.Count());
            foreach (var entity in data)
            {
                var log = ApprovedPolicyService
                    .GetCurrentApprovalLog(entity.Id)
                    .Adapt<Vo.ApproLog>();
                res.Add(new(entity.Adapt<VoT>(), log));
            }
            return TypedResults.Ok(res);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public virtual Results<Ok<Tuple<VoT, Vo.ApproLog>>, BadRequest<string>> GetDataById(long id)
    {
        try
        {
            return TypedResults.Ok(
                new Tuple<VoT, Vo.ApproLog>(
                    BasicEntityService.GetEntityById(id).Adapt<VoT>(),
                    ApprovedPolicyService.GetCurrentApprovalLog(id).Adapt<Vo.ApproLog>()
                )
            );
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}
