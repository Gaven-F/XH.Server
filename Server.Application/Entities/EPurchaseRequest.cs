using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EPurchaseRequest : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string ProcureProject { get; set; } = string.Empty;
    public string ProcurType { get; set; } = string.Empty;
    public string ExplanatExpenditure { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public string TotalAmount { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
    public string ProcureMethod { get; set; } = string.Empty;
    public string LargeAmount { get; set; } = string.Empty;
    public string AmountType { get; set; } = string.Empty;
}