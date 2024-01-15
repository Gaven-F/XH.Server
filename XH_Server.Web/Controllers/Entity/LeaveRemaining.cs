using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Entities;
using XH_Server.Domain.Repository;

namespace XH_Server.Web.Controllers.Entity;
/// <summary>
/// 假期剩余
/// </summary>
public class LeaveRemaining(IRepositoryService<ELeaveRemaining> repository) : IDynamicApiController
{

	public int GetLeaveDate(string userId, string leaveType)
	{
		return repository.GetData(false)
			.FirstOrDefault(it => it.UserId == userId && it.LeaveType == leaveType)?.Cnt ?? 0;
	}

	[HttpPost]
	public void SetLeaveDate(ELeaveRemaining data)
	{
		repository.SaveData(data);
	}

	[HttpPost]
	public void SetLeaveDates(IEnumerable<ELeaveRemaining> data)
	{
		foreach (var item in data)
		{
			repository.SaveData(item);
		}
	}

	[HttpPut]
	public void Update(string id, int cnt)
	{
		var e = repository.GetDataById(Convert.ToInt64(id));
		e.Cnt = cnt;
		repository.UpdateData(e);
	}
}
