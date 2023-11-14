using SqlSugar;

namespace XH_Server.Domain.Entities;

public class ApprovalStep : BaseEntity
{
    public string? ApproverId { get; set; }
    public string? ApproverLevel { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }
    public string ApprovalDescription { get; set; } = "无";

    public StepType StepType { get; set; }

    public long LastStepId { get; set; }

    [Navigate(NavigateType.OneToMany, nameof(LastStepId))]
    public List<ApprovalStep>? ChildSteps { get; set; }
}


public enum ApprovalStatus
{
    Normal,
    Pass,
    Back
}

public enum StepType
{
    Approve,
    Read
}

