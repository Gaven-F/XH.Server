using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approval;

public class EApprovalTemplate : BasicEntity
{
	public long StartNodeId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	[SugarColumn(ColumnDescription = "绑定的业务数据名")] public string BindDataName { get; set; } = string.Empty;
	[SugarColumn(ColumnDescription = "绑定的业务数据显示名")] public string BindDataDisplayName { get; set; } = string.Empty;

	[Navigate(NavigateType.OneToOne, nameof(StartNodeId))]
	public EApprovalNode? StartNode { get; set; }
}


