using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 付款
/// </summary>
public class Payment : BasicApplicationApi<EPayment>, IDynamicApiController
{
    [HttpPut("{id}")]
    public IResult Update(string id, string sourcesFunding)
    {
        long _id = Convert.ToInt64(id);
        var has = Db.Instance.Queryable<EPayment>().InSingle(_id);
        if (has == null)
        {
            return Results.BadRequest("数据不存在！");
        }
        has.SourcesFunding = sourcesFunding;
        has.UpdateTime = DateTime.Now;

        return Results.Ok(Db.Instance.Updateable(has).ExecuteCommand());
    }
}
