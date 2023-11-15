using XH_Server.Domain.Entities;

namespace XH_Server.Domain.Dto;

public class ApprovalStepDto
{
    public List<string>? ApproverId { get; set; }
    public List<string>? ApproverLevel { get; set; }

    public StepType StepType { get; set; }
}
