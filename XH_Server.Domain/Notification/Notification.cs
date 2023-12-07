namespace XH_Server.Domain.Notification;
public interface INotificationService
{
	public static readonly string MsgTemplate =
		"""
		[{id}]
		创建日期：{currentTime}
		创建人部门：{creatorDepartment}
		创建人：{creator}
		""";

	public void SendDingTalkMsg(string receiver, string title, string msg);
	public void SendEmailMsg(string receiver, string title, string msg);
}

// TODO 实现接口
public class NotificationService()
{
}