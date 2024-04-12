using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EConsumableManagement : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string ConsumableName { get; set; } = string.Empty;
    public string ConsumableLevel { get; set; } = string.Empty;
    public DateTime UsageTime { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string EquipmentInvolved { get; set; } = string.Empty;
}
