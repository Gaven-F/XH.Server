using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH.Core.DataBase.Tables;

namespace XH.Core.Database.Tables.ApprovalTables;
/// <summary>
/// 审核步骤数据实例
/// </summary>
public class ApprovalStep : BaseEntry
{
	[SugarColumn(ColumnDescription = "审核步骤名称")]
	public string Name { get; set; } = string.Empty;

	[SugarColumn(ColumnDescription = "上一审核步骤", ColumnDataType = "JSON")]
	public string LastSteps { get; set; } = string.Empty;
	[SugarColumn(ColumnDescription = "下一审核步骤", ColumnDataType = "JSON")]
	public string NextSteps { get; set; } = string.Empty;

	[SugarColumn(ColumnDescription = "审核人")]
	public string? Approver { get; set; }
	[SugarColumn(ColumnDescription = "审核状态")]
	public ApprovalStatusEnum ApprovalStatus { get; set; } = ApprovalStatusEnum.Normal;
	[SugarColumn(ColumnDescription = "审核反馈")]
	public string ApprovalMsg { get; set; } = "无";

}

public enum ApprovalStatusEnum
{
	Normal,
	Pass,
	Back
}
