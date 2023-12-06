namespace XH_Server.Domain.Entities;

/// <summary>
/// 基本实体接口
/// </summary>
/// <remake>
/// 因为需要在外部使用SqlSugar的部分特性，所以这里使用接口而不是类。
///	具体的实现在外部进行
/// </remake>
public interface IBasicEntity
{
	/// <summary>
	/// 唯一Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 创建时间
	/// </summary>
	public DateTimeOffset CreateTime { get; set; }
	/// <summary>
	/// 更新时间
	/// </summary>
	public DateTimeOffset UpdateTime { get; set; }
	/// <summary>
	/// 逻辑删除位
	/// </summary>
	public bool IsDelete { get; set; }
}
