using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application;
using XH_Server.Application.Entities;
using XH_Server.Application.Entities.Dto;
using XH_Server.Core.Database;
using XH_Server.Domain.ApprocedPolicy;

namespace XH_Server.Web.Controllers.Entity;

/// <summary>
/// 订单_V2
/// </summary>
public class Order : BasicApplicationApi<EOrder, EOrder>, IDynamicApiController
{
	public override Results<Ok<string>, BadRequest<string>> Add(EOrder entity)
	{
		try
		{
			var id = BasicEntityService.GetDb().Instance.InsertNav(entity).Include(it => it.Items).ExecuteReturnEntity().Id;
			ApprovedPolicyService.CreateApproveBasicLog(entity);
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

	public override Results<Ok<List<Tuple<EOrder, EApprovalLog>>>, BadRequest<string>> GetData()
	{
		try
		{
			var data = BasicEntityService.GetDb().Instance
								.Queryable<EOrder>()
								.Where(it => !it.IsDeleted)
								.Includes(it => it.Items)
								.ToList();
			List<Tuple<EOrder, EApprovalLog>> res = new(data.Count);
			foreach (var entity in data)
			{
				var log = ApprovedPolicyService.GetCurrentApprovalLog(entity.Id).Adapt<Vo.ApproLog>();
				res.Add(new(entity, log));
			}
			return TypedResults.Ok(res);
		}
		catch (Exception e)
		{
			return TypedResults.BadRequest(e.Message);
		}
	}

	[HttpGet("{engineerId}")]
	public ActionResult<IEnumerable<EOrder>> GetDataByEngineer(string engineerId)
	{
		var data = BasicEntityService.GetDb().Instance
			.Queryable<EOrder>()
			.Where(it => !it.IsDeleted)
			.Includes(it => it.Items)
			.ToList();
		var res = data.Where(d => d.Items != null && d.Items.Where(i => i.Engineer == engineerId).Any());
		return res.ToList();
	}

	public override Results<Ok<Tuple<EOrder, Vo.ApproLog>>, BadRequest<string>> GetDataById(long id)
	{
		var data = BasicEntityService.GetDb().Instance
			.Queryable<EOrder>()
			.Includes(it => it.Items)
			.Where(it => !it.IsDeleted)
			.InSingle(id);

		if (data == null)
		{
			return TypedResults.BadRequest("未找到实例！");
		}

		var log = ApprovedPolicyService.GetCurrentApprovalLog(id);

		return TypedResults.Ok(new Tuple<EOrder, Vo.ApproLog>(data, log.Adapt<Vo.ApproLog>()));
	}

	[HttpPut("{id}")]
	public ActionResult UpdateItem(string id, EOrderItem item)
	{
		var db = BasicEntityService.GetDb();
		var _id = Convert.ToInt64(id);
		if (_id != item.Id) { return new BadRequestObjectResult("id不匹配！"); }
		item.UpdateTime = DateTime.Now;
		var res = db.Instance.Updateable(item).ExecuteCommand();
		return new OkObjectResult(res);
	}

	[HttpGet]
	public IResult DownloadFile([FromServices] DatabaseService dbService, string orderId)
	{

		var id = Convert.ToInt64(orderId);
		var db = dbService.Instance;
		var data = db.Queryable<EOrder>()
		  .Includes(it => it.Items)
		  .Where(it => !it.IsDeleted)
		  .InSingle(id);

		if (data == null) { return Results.BadRequest(); };
		var stream = Utils.OrderUtils.ReplaceByEntity(data);
		return TypedResults.Stream(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "demo.docx");
	}
}
