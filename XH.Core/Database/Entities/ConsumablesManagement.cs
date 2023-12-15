using System;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;

public class ConsumablesManagement : BaseEntity
{
	public string? CorpId { get; set; }
	public string? ConsumableName { get; set; }
	public string? ConsumableLevel { get; set; }
	public DateTime? UsageTime { get; set; }
	public string? Purpose { get; set; }
	public string? EquipmentInvolved { get; set; }
}
