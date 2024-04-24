namespace Server.Application.Entities;

public class EPayment : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string ReasonPayment { get; set; } = string.Empty;
	public string PaymentType { get; set; } = string.Empty;
	public string Amount { get; set; } = string.Empty;
	public string PaymentMethods { get; set; } = string.Empty;
	public DateTime DatePayment { get; set; }
	public string PurchaseDetails { get; set; } = string.Empty;
	public string ReceivingUnit { get; set; } = string.Empty;
	public string Bank { get; set; } = string.Empty;
	public string Account { get; set; } = string.Empty;
	public string PaymentInstructions { get; set; } = string.Empty;
	public string SourcesFunding { get; set; } = string.Empty;
	public string Invoicing { get; set; } = string.Empty;
	public string Annex { get; set; } = string.Empty;
	public string Remark { get; set; } = string.Empty;
	public string LargeAmount { get; set; } = string.Empty;
	public string ContractNumber { get; set; } = string.Empty;
	public string AssociateOtherId { get; set; } = string.Empty;
	public string AssociateOtherType { get; set; } = string.Empty;
}
