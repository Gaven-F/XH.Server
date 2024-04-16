namespace Server.Domain.Basic;

public class EConsumableManage : BasicEntity
{
    public const string SERIAL_NUMBER = "编号";
    public const string NAME = "名称";
    public const string GRADE = "等级";
    public const string USE = "用途";
    public const string UNIT_PRICE = "单价";
    public const string QUANTITY = "数量";
    public const string TOTAL_PRICE = "总价";
    public const string REMARKS = "备注";

    /// <summary>
    /// 编号
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public string? Grade { get; set; }

    /// <summary>
    /// 用途
    /// </summary>
    public string? Use { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public string? UnitPrice { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public string? Quantity { get; set; }

    /// <summary>
    /// 总价
    /// </summary>
    public string? TotalPrice { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remarks { get; set; }
}
