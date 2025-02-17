﻿namespace Server.Application.Entities;

public class ETopic : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
#pragma warning disable CRRSP08
    public string ApplicatDepart { get; set; } = string.Empty;
#pragma warning restore CRRSP08
    public string TopicTitle { get; set; } = string.Empty;
    public string TopicContent { get; set; } = string.Empty;
    public string ReviewConference { get; set; } = string.Empty;
    public DateTime MeetingTime { get; set; }
    public string Participants { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string LawyerApproval { get; set; } = string.Empty;
}
