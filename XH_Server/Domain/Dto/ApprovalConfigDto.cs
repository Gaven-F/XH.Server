using SqlSugar;
using XH_Server.Domain.Entities.Extra;
using XH_Server.Domain.Vo;

namespace XH_Server.Domain.Dto;

public class ApprovalConfigDto
{
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty;
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public string FiledName { get; set; } = string.Empty;

    public List<DingTalkUserInfo> Users { get; set; } = new List<DingTalkUserInfo>();

    public List<DingTalkUserInfo> Approver { get; set; } = new List<DingTalkUserInfo>();

    public List<DingTalkUserInfo> CopyTo { get; set; } = new List<DingTalkUserInfo>();
}
