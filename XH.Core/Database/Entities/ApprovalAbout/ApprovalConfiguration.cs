using SqlSugar;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities.ApprovalEntities;
public class ApprovalConfiguration : BaseEntity
{
    [SugarColumn(ColumnDescription = "审核流程配置名称", ColumnDataType = "varchar(255)")]
    public string Name { get; set; } = string.Empty;
    [SugarColumn(ColumnDescription = "审核流程配置", ColumnDataType = "JSON")]
    public string Config { get; set; } = string.Empty;
}
