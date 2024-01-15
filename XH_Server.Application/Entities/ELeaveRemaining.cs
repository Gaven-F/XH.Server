using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;
public class ELeaveRemaining : BasicEntity
{
	public string UserId { get; set; } = string.Empty;
	public string LeaveType { get; set; } = string.Empty;
	public int Cnt { get; set; }
}
