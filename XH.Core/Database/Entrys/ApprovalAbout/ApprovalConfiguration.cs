using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH.Core.DataBase.Tables;

namespace XH.Core.Database.Tables.ApprovalTables;
public class ApprovalConfiguration : BaseEntry
{
	[SugarColumn(ColumnDescription = "审核流程配置名称", ColumnDataType = "varchar(255)")]
	public string Name { get; set; } = string.Empty;
	[SugarColumn(ColumnDescription = "审核流程配置", ColumnDataType = "JSON")]
	public string Config { get; set; } = string.Empty;
}
