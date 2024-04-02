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

    public ELeaveRemaining GetLeaveDate(string userId, string leaveType)
    {
        return repository.GetData(false)
            .FirstOrDefault(it => it.UserId == userId && it.LeaveType == leaveType) ?? new ELeaveRemaining();
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

    [HttpPut]
    public void Minus(string id, int cnt)
    {
        var e = repository.GetDataById(Convert.ToInt64(id));
        e.Cnt -= cnt;
        repository.UpdateData(e);
    }

    [HttpDelete("{id}")]
    public int Delete(string id)
    {
        var i = Convert.ToInt64(id);
        var data = repository.GetDataById(i);
        data.IsDeleted = true;
        return repository.UpdateData(data);
    }
}
