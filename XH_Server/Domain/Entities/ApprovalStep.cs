using SqlSugar;
using XH_Server.Domain.Entities.Extra;

namespace XH_Server.Domain.Entities;

public class ApprovalStep
{
    public DingTalkUserInfo? Approcer { get; set; }


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

