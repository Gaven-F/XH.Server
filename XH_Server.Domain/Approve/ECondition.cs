using XH_Server.Common.Condition;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approve;
public class ECondition : BasicEntity
{
	public long NodeId { get; set; }
	public Condition? Condition { get; set; }
	public long PassNodeId { get; set; }
	public long BackNodeId { get; set; }
}
