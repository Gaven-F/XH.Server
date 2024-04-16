using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 设备绑定
/// </summary>
public class Bind : BasicApplicationApi<EBind>, IDynamicApiController
{
    [NonAction]
    public override Results<Ok<bool>, BadRequest<string>> Approve(
        long logId,
        byte status,
        string msg = "无"
    )
    {
        return base.Approve(logId, status, msg);
    }
}
