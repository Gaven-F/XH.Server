using SqlSugar;

namespace XH_Server.Domain.Basic;

public class BasicEntity
{
	[SugarColumn(IsPrimaryKey = true)]
	public long Id { get; set; }
	public DateTime CreateTime { get; set; } = DateTime.Now;
	public DateTime UpdateTime { get; set; } = DateTime.Now;
	public bool IsDeleted { get; set; } = false;
}
