using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EMeetingMinutes : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string MeetingNumber { get; set; } = string.Empty;
	public string SummaryName { get; set; } = string.Empty;
	public DateTime MeetingDate { get; set; }
	public string MeetingAttachments { get; set; } = string.Empty;
	public string MeetingMinutes { get; set; } = string.Empty;
	public string Notes { get; set; } = string.Empty;
}

