using Server.Domain.Basic;

namespace Server.Application.Entities;

public class ELeaveRemaining : BasicEntity
{
    public string UserId { get; set; } = string.Empty;
    public string LeaveType { get; set; } = string.Empty;
    public double Cnt { get; set; }
}
