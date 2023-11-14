namespace XH_Server.Domain.Vo;

public class ApprovalConfigVo
{
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public string FiledName { get; set; } = string.Empty;

    public List<ApprovalStepVo>? ApprovalTemplate { get; set; }
}
