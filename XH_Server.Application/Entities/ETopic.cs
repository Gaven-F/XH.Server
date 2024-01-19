using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;
public class ETopic : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string ApplicatDepart { get; set; } = string.Empty;
	public string TopicTitle { get; set; } = string.Empty;
	public string TopicContent { get; set; } = string.Empty;
	public string ReviewConference { get; set; } = string.Empty;
	public DateTime MeetingTime { get; set; }
	public string Participants { get; set; } = string.Empty;
	public string Annex { get; set; } = string.Empty;
	public string Notes { get; set; } = string.Empty;
	public string LawyerApproval { get; set; } = string.Empty;

}
