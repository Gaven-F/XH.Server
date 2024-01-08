using SqlSugar;
using System;
using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;

public class Topic : BaseEntity
{
    public string? CorpId { get; set; }
    public string? TopicTitle { get; set; }
	public string? TopicContent { get; set; }
	public DateTime MeetingTime { get; set; }
	public string? Annex { get; set; }
	public string? LawyerApproval { get; set; }
	[SugarColumn(IsJson = true)]
	public List<string>? Participants { get; set; }
}
