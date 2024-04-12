using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EMeetingRoomApplication : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string AffiliatedCompany { get; set; } = string.Empty;
    public DateTime ApplicationTime { get; set; }
    public DateTime UsageTime { get; set; }
    public string ReasonBorrow { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
}