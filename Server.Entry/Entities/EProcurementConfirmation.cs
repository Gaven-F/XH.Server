namespace Server.Application.Entities;

public class EProcurementConfirmation : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProcurType { get; set; } = string.Empty;
    public DateTime DeliveryTime { get; set; }
    public string ProcurementDetails { get; set; } = string.Empty;
    public string ProcurName { get; set; } = string.Empty;
    public string PurchaseQuantity { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string UnitPrice { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string AssociateOtherId { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
}
