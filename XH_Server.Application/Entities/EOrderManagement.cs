using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EOrderManagement : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string OrderNumber { get; set; } = string.Empty;
	public string OrderDetails { get; set; } = string.Empty;
	public string SampleNumber { get; set; } = string.Empty;
	public string CustomerInformation { get; set; } = string.Empty;
	public DateTime StartTime { get; set; }
	public string ProjectName { get; set; } = string.Empty;
	public string ProjectLeader { get; set; } = string.Empty;
	public string EquipmentInvolved { get; set; } = string.Empty;
}

