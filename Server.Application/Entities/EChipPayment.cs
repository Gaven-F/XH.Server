using Server.Domain.Basic;

namespace Server.Application.Entities;

public class EChipPayment : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string ContractNumber { get; set; } = string.Empty;
    public string ReasonPayment { get; set; } = string.Empty;
    public DateTime DatePayment { get; set; }
    public string PaymentMethods { get; set; } = string.Empty;
    public string ReceivingUnit { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string RateSelection { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public string LargeAmount { get; set; } = string.Empty;
    public string TotalAmount { get; set; } = string.Empty;
    public string PaymentType { get; set; } = string.Empty;
    public string ProofPayment { get; set; } = string.Empty;
    public string Advances { get; set; } = string.Empty;
    public string ContractOrder { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
}
