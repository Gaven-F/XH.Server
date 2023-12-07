using XH_Server.Common.Condition;

namespace XH_Server.Domain.Approval;
public class EApprovalConditions
{
	public long NodeId { get; set; }
	public long NextNodeId { get; set; }
	/*
	 * condition template:
	 * [judgment filed] [e(qual)|g(reater)|l(ess)|ge|le|ne] [value]
	 */
	[SugarColumn(ColumnDataType = "VarChar", SqlParameterDbType = typeof(ConditionConvert))]
	public Condition? Condition { get; set; }

	[Navigate(NavigateType.OneToOne, nameof(NextNodeId))]
	public EApprovalNode? NextNode { get; set; }
	public bool IsDefault { get; set; } = false;
}
