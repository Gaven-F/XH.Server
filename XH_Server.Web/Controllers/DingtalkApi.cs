using Microsoft.AspNetCore.Mvc;

namespace XH_Server.Web.Controllers;

[ApiDescriptionSettings(Order = 99)]
public class DingtalkApi(DingtalkUtils.DingtalkUtils dingtalkUtils) : ControllerBase
{
	public IEnumerable<string>? GetAllUserId()
	{
		return dingtalkUtils.GetUserIds();
	}

	public IEnumerable<string>? GetUserInfo([FromQuery]IEnumerable<string> userId, [FromQuery] IEnumerable<string>? fileds )
	{
		return dingtalkUtils.GetUserInfo(userId, fileds);
	}

	public void SendMsg(IEnumerable<string> userId, string msg)
	{
		dingtalkUtils.SendMsg(userId, msg);
	}
}
