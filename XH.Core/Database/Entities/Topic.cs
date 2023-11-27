using SqlSugar;
using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;

public class Topic : BaseEntity
{
	public string? CorpId { get; set; }
	public string? ApplicatDepart { get; set; }
	public string? TopicTitle { get; set; }
	public string? TopicContent { get; set; }
	public string? ReviewConference { get; set; }
	public DateTimeOffset MeetingTime { get; set; }
	[SugarColumn(IsJson = true, ColumnDataType = "JSON")]

	public List<string>? Participants { get; set; }
	[SugarColumn(IsJson = true, ColumnDataType = "JSON")]

	public List<string>? Annex { get; set; }
	public string? Notes { get; set; }
	public string? LawyerApproval { get; set; }
	public string? State { get; set; }
}
