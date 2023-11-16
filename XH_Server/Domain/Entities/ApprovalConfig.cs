using SqlSugar;
using XH_Server.Domain.Dto;
using XH_Server.Domain.Entities.Extra;

namespace XH_Server.Domain.Entities;

public class ApprovalConfig : BaseEntity
{
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty;
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public string FiledName { get; set; } = string.Empty;

    [SugarColumn(ColumnDataType = "JSON", IsJson = true)]
    public List<DingTalkUserInfo> Users { get; set; } = new List<DingTalkUserInfo>();

    [SugarColumn(ColumnDataType = "JSON", IsJson = true)]
    public List<DingTalkUserInfo> Approver { get; set; } = new List<DingTalkUserInfo>();

    [SugarColumn(ColumnDataType = "JSON", IsJson = true)]
    public List<DingTalkUserInfo> CopyTo { get; set; } = new List<DingTalkUserInfo>();

    //[SugarColumn(ColumnDataType = "JSON", IsJson = true, ColumnDescription = "审核流程模板")]
    //public List<ApprovalStepDto>? ApprovalTemplate { get; set; }
}
