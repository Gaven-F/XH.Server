using XH_Server.Domain.Entities;

namespace XH_Server.Domain.Vo;

public class ApprovalStepVo
{
    public List<string>? ApproverId { get; set; }
    public List<string>? ApproverLevel { get; set; }

    public StepType StepType { get; set; }
    public ApprovalStatus Status { get; set; }
}
