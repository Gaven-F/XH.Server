namespace Server.Domain.Basic;

public class EUserInfo : BasicEntity
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 本为DingTalk的UserId，但是因为前端的原因更名为CorpId
    /// </summary>
    public string CropId { get; set; } = string.Empty;

    /// <summary>
    /// 职位
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; } = string.Empty;
}
