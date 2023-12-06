using XH_Server.Domain.Entities;

namespace XH_Server.Demo;

internal class BaseEntity : IBasicEntity
{
	[SugarColumn(IsPrimaryKey = true)]
	public long Id { get; set; }
	[SugarColumn(ColumnDataType = "DateTime")]
	public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;
	[SugarColumn(ColumnDataType = "DateTime")]
	public DateTimeOffset UpdateTime { get; set; } = DateTimeOffset.Now;
	public bool IsDelete { get; set; } = false;

	public string Data { get; set; } = "NONE";
}
