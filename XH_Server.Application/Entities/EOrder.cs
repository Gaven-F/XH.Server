using SqlSugar;
using XH_Server.Domain.Basic;
namespace XH_Server.Application.Entities;

public class EOrder : BasicEntity
{
	// 申请人
	public string OrpId { get; set; } = string.Empty;
	// 编号
	public string Umbering { get; set; } = string.Empty;
	// 随工单号
	public string OrkNumber { get; set; } = string.Empty;
	// 客户名称
	public string UstomerName { get; set; } = string.Empty;
	// 委托工程师
	public string OmmissioneEngineers { get; set; } = string.Empty;
	// 联系电话
	public string OntactNumber { get; set; } = string.Empty;
	// 产品数量
	public string RoductsNumber { get; set; } = string.Empty;
	// 产品名称
	public string RoductsName { get; set; } = string.Empty;
	// 产品型号
	public string RoductsModel { get; set; } = string.Empty;
	// 产品批次
	public string RoductsLots { get; set; } = string.Empty;
	// 封装形式
	public string OrmFactor { get; set; } = string.Empty;
	// 现场
	public string Cene { get; set; } = string.Empty;
	// 紧急程度
	public string Rgency { get; set; } = string.Empty;
	// 完成时间
	public DateTime OmpletionTime { get; set; }
	// 温度
	public string Emperature { get; set; } = string.Empty;
	// 湿度
	public string Humidity { get; set; } = string.Empty;


	[Navigate(NavigateType.OneToMany, nameof(EOrderItem.OrderId), nameof(Id))]
	public List<EOrderItem>? Items { get; set; }
}

public class EOrderItem : BasicEntity
{
	public long OrderId { get; set; }
	public string Project { get; set; } = string.Empty;
	public string Condition { get; set; } = string.Empty;
	public string Engineer { get; set; } = string.Empty;
	public string Quantity { get; set; } = string.Empty;
	public string DeviceNumber { get; set; } = string.Empty;
	public string StartTime { get; set; } = string.Empty;
	public string VirtualStartTime { get; set; } = string.Empty;
	public string EndTime { get; set; } = string.Empty;
	public string VirtualEndTime { get; set; } = string.Empty;
	public string TrialTime { get; set; } = string.Empty;
	public string VirtualTrialTime { get; set; } = string.Empty;
	public string Sign { get; set; } = string.Empty;
}
