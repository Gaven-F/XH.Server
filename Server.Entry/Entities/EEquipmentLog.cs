namespace Server.Application.Entities;

public class EEquipmentLog : BasicEntity
{
    /// <summary>
    /// 样品Id
    /// </summary>
    public string GoodsID { get; set; } = string.Empty;

    /// <summary>
    /// 扫描枪编号
    /// </summary>
    public string EquipmentId { get; set; } = string.Empty;

    [SqlSugar.SugarColumn(DefaultValue = "0001-1-1 00:00:00")]
    public DateTime EndTime { get; set; } = DateTime.Now;
    public string? Type { get; set; }

    /// <summary>
    /// 绑定的样品
    /// </summary>
    /// <remarks>
    /// 类型为E（设备）则有相关的绑定样品
    /// </remarks>
    public string? BindS { get; set; }
    public string? Info { get; set; }
    public string? Operate { get; set; }
}
