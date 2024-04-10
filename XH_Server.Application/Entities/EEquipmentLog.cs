using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;
public class EEquipmentLog : BasicEntity
{
    public string GoodsID { get; set; } = string.Empty;
    public string EquipmentId { get; set; } = string.Empty;
    [SqlSugar.SugarColumn(DefaultValue = "0001-1-1 00:00:00")]
    public DateTime EndTime { get; set; } = DateTime.Now;
    public string? Type { get; set; }
    public string? BindS { get; set; }
    public string? Info { get; set; }
    public string? Operate { get; set; }
}
