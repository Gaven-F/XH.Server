using XH_Server.Domain.Vo;

namespace XH_Server.Domain.Entities;

public class ApprovalConfig : BaseEntity
{
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public string FiledName { get; set; } = string.Empty;

    [SqlSugar.SugarColumn(ColumnDataType = "JSON", IsJson = true, ColumnDescription = "审核流程模板")]
    public List<ApprovalStepVo>? ApprovalTemplate { get; set; }
}
