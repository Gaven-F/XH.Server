using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approve;
public class EApprovalTemplate : BasicEntity
{
    public string Name { get; set; } = string.Empty;
    public string Remake { get; set; } = string.Empty;
    public string BindEntity { get; set; } = string.Empty;
    public long StartNodeId { get; set; }
}
