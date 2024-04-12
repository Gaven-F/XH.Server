using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EMeetingMinutes : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string MeetingNumber { get; set; } = string.Empty;
    public string SummaryName { get; set; } = string.Empty;
    public DateTime MeetingDate { get; set; }
    public string MeetingAttachments { get; set; } = string.Empty;
    public string MeetingMinutes { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string AssociateOtherId { get; set; } = string.Empty;
}
