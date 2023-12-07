using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH_Server.Domain.Basic;
using XH_Server.Domain.DataEntities;

namespace XH_Server.Domain.Approval;
public class EApprovalNode : BasicEntity
{
	public NodeType Type { get; set; } = NodeType.Approval;
	public string UserId { get; set; } = string.Empty;
	public long NextNodeId { get; set; }

	[Navigate(NavigateType.OneToOne, nameof(UserId), nameof(EStaff.UserId))]
	public EStaff? Staff { get; set; }
	[Navigate(NavigateType.OneToMany, nameof(EApprovalConditions.NodeId))]
	public List<EApprovalConditions>? Conditions { get; set; }
    [Navigate(NavigateType.OneToOne, nameof(NextNodeId))]
    public EApprovalNode? NextNode { get; set; }
}

[Flags]
public enum NodeType
{
	Approval, Copy, Branch
}