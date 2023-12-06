using System.Collections.Generic;


namespace XH.Core.DataBase.Entities;

public class ProjectManagement : BaseEntity
{
	public string? CorpId { get; set; }
	public string? ItemNumber { get; set; }
	public string? MainContent { get; set; }
	public string? ProjectLeader { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Annex { get; set; }
	public string? ProjectName { get; set; }
	public string? Remark { get; set; }
}
