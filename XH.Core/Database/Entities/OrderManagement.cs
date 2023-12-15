using System;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class OrderManagement : BaseEntity
{
	public string? CorpId { get; set; }
	public string? OrderDetails { get; set; }
	public string? SampleNumber { get; set; }
	public string? CustomerInformation { get; set; }
	public DateTime? StartTime { get; set; }
	public string? ProjectName { get; set; }
	public string? ProjectLeader { get; set; }
	public string? EquipmentInvolved { get; set; }
}
