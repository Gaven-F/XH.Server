using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 假期剩余
/// </summary>
public class LeaveRemaining(IRepositoryService<ELeaveRemaining> repository)
    : BasicApplicationApi<ELeaveRemaining>,
        IDynamicApiController
{
    public ELeaveRemaining GetLeaveDate(string userId, string leaveType) =>
        repository
            .GetData(false)
            .FirstOrDefault(it => it.UserId == userId && it.LeaveType == leaveType)
        ?? new ELeaveRemaining();

    [HttpPost]
    public void SetLeaveDate(ELeaveRemaining data) => repository.SaveData(data);

    [HttpPost]
    public void SetLeaveDates(IEnumerable<ELeaveRemaining> data)
    {
        foreach (var item in data)
        {
            repository.SaveData(item);
        }
    }

    [HttpPut]
    public void Update(string id, double cnt)
    {
        var e = repository.GetDataById(Convert.ToInt64(id));
        e.Cnt = cnt;
        repository.UpdateData(e);
    }

    [HttpPut]
    public void Minus(string id, double cnt)
    {
        var e = repository.GetDataById(Convert.ToInt64(id));
        e.Cnt -= cnt;
        repository.UpdateData(e);
    }

    [NonAction]
    public override Results<Ok<bool>, BadRequest<string>> Approve(
        long logId,
        byte status,
        string msg = "无"
    )
    {
        return base.Approve(logId, status, msg);
    }

    [NonAction]
    public override Results<Ok<string>, BadRequest<string>> Add(ELeaveRemaining entity)
    {
        return base.Add(entity);
    }
}
