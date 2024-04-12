using Server.Domain.Basic;

namespace Server.Application.Entities;

public class ESalaryInfo : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string MainContent { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public string Annex { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
}
