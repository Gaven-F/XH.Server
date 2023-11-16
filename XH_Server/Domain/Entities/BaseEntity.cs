using SqlSugar;

namespace XH_Server.Domain.Entities;

public class BaseEntity
{
    [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键")]
    public long Id { get; set; }
    [SugarColumn(ColumnDescription = "创建时间")]
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;
    [SugarColumn(ColumnDescription = "修改时间")]
    public DateTimeOffset UpdateTime { get; set; } = DateTimeOffset.Now;
    [SugarColumn(ColumnDescription = "逻辑删除")]
    public bool IsDelete { get; set; }
}
