using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EUseSeal : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string StampType { get; set; } = string.Empty;
    public string ApplicatDepart { get; set; } = string.Empty;
    public string HandledBy { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string ReasonBorrow { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SealType { get; set; } = string.Empty;
}