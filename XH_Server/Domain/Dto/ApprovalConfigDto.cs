using XH_Server.Domain.Vo;

namespace XH_Server.Domain.Dto;

public class ApprovalConfigDto
{
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty;
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public string FiledName { get; set; } = string.Empty;

    public List<ApprovalStepDto>? ApprovalTemplate { get; set; }
}
