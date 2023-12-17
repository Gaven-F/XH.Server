﻿using System;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class EquipmentLog : BaseEntity
{
	public string GoodsID { get; set; } = string.Empty;
	public string EquipmentId { get; set; } = string.Empty;
	[SqlSugar.SugarColumn(DefaultValue = "0001-1-1 00:00:00")]
	public DateTimeOffset EndTime { get; set; } = DateTime.Now;
	public string? Type { get; set; }
	public string? BindS { get; set; }
	public string? Info { get; set; }
}
