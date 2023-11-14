using SqlSugar;
using System;

namespace XH.Core.DataBase.Entities;
public class BaseEntity
{
    [SugarColumn(IsPrimaryKey = true, ColumnDescription = "唯一主键")]
    public long Id { get; set; }

    [SugarColumn(ColumnDescription = "创建时间")]
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

    [SugarColumn(ColumnDescription = "上一次更新时间")]
    public DateTimeOffset UpdateTime { get; set; } = default;

    [SugarColumn(ColumnDescription = "删除时间")]
    public DateTimeOffset DeleteTime { get; set; } = default;

    [SugarColumn(ColumnDescription = "逻辑删除")]
    public bool IsDelete { get; set; } = default;
}
