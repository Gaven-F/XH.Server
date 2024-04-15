using Server.Domain.ApprovedPolicy.Condition;
using Server.Domain.Basic;

namespace Server.Domain.ApprovedPolicy;

public class EApprovedPolicy : BasicEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;

    [SqlSugar.Navigate(SqlSugar.NavigateType.OneToMany, nameof(ECondition.PolicyId))]
    public List<ECondition>? Conditions { get; set; }

    public bool IsDefault { get; set; }

    public string ApproverIds { get; set; } = "";

    //public string ApproveLevels { get; set; } = "";
    public string CopyIds { get; set; } = "";
}
