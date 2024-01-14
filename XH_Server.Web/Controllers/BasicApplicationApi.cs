using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Entities.Dto;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;

namespace XH_Server.Application;
#pragma warning disable 8618
public class BasicApplicationApi<T, DtoT>(
	IBasicEntityService<T> basicEntityService,
	ApprovedPolicyService approvedPolicyService
	)
	where T : BasicEntity
{
#pragma warning restore
	[FromServices]
	public DingtalkUtils.DingtalkUtils DingtalkUtils { get; set; }

	public virtual Results<Ok<string>, BadRequest<string>> Add(T entity)
	{
		try
		{
			var id = basicEntityService.Create(entity);
			approvedPolicyService.CreateApproveBasicLog(entity);
			var log = approvedPolicyService.GetCurrentApprovalLog(id);
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

	public virtual Results<Ok<bool>, BadRequest<string>> Approve(long logId, byte status, string msg = "无")
	{
		try
		{

			var eId = approvedPolicyService.GetLogById(logId).EntityId;
			approvedPolicyService.Approve(logId, status, msg);
			var cLog = approvedPolicyService.GetCurrentApprovalLog(eId);

			if (cLog == null)
			{
				var e = basicEntityService.GetEntityById(eId);
				DingtalkUtils.SendMsg(approvedPolicyService.GetPolicy<T>(eId).CopyIds.Split(','), $"""
				抄送信息：
				{e.CreateTime.ToLongDateString()}
				""");

				return TypedResults.Ok(true);
			}

			DingtalkUtils.SendMsg([cLog.ApproverId.ToString()], "有一个待审核的消息！");

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
			return TypedResults.Ok(basicEntityService.Delete(eId));
		}
		catch (Exception e)
		{
			return TypedResults.BadRequest(e.Message);
		}
	}

	public Results<Ok<List<Tuple<DtoT, EApprovalLog>>>, BadRequest<string>> GetData()
	{
		try
		{
			var data = basicEntityService.GetEntities();
			List<Tuple<DtoT, EApprovalLog>> res = new(data.Count());
			foreach (var entity in data)
			{
				var log = approvedPolicyService.GetCurrentApprovalLog(entity.Id).Adapt<Dtos.ApproLog>();
				res.Add(new(entity.Adapt<DtoT>(), log));
			}
			return TypedResults.Ok(res);
		}
		catch (Exception e)
		{
			return TypedResults.BadRequest(e.Message);
		}
	}

	public virtual Results<Ok<Tuple<DtoT, Dtos.ApproLog>>, BadRequest<string>> GetDataById(long id)
	{
		try
		{
			return TypedResults.Ok(new Tuple<DtoT, Dtos.ApproLog>(
				basicEntityService.GetEntityById(id).Adapt<DtoT>(),
				approvedPolicyService.GetCurrentApprovalLog(id).Adapt<Dtos.ApproLog>()));
		}
		catch (Exception e)
		{
			return TypedResults.BadRequest(e.Message);
		}
	}
}

