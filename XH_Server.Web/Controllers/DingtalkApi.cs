using Microsoft.AspNetCore.Mvc;

namespace XH_Server.Web.Controllers;

[ApiDescriptionSettings(Order = 99)]
public class DingtalkApi : ControllerBase
{
	public IEnumerable<string> GetAllUserId()
	{
		return DingtalkUtils.GetUserIds();
	}

	public IEnumerable<string> GetUserInfo(IEnumerable<string> userId, IEnumerable<string>? fileds)
	{
		return DingtalkUtils.GetUserInfo(userId, fileds);
	}

	public void SendMsg(IEnumerable<string> userId, string msg)
	{
		DingtalkUtils.SendMsg(userId, msg);
	}
}
