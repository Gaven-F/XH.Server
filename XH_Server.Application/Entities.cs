using XH_Server.Domain.Basic;

namespace XH_Server.Application;
public class Entities
{
	public class EBussinessTrip : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string TravelType { get; set; } = string.Empty;
		public string TypeDependent { get; set; } = string.Empty;
		public string PlaceDeparture { get; set; } = string.Empty;
		public string Destination { get; set; } = string.Empty;
		public string Vehicle { get; set; } = string.Empty;
		public string Colleague { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string SumTime { get; set; } = string.Empty;
		public string BusinessTrip { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

	public class ELeave : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string LeaveType { get; set; } = string.Empty;
		public string AnnualLeave { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string SumTime { get; set; } = string.Empty;
		public string EndDateSuffix { get; set; } = string.Empty;
		public string StartDateSuffix { get; set; } = string.Empty;

        public string ReasonLeave { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

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
	}

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
		public string ResultReport { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

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

	public class EInvoicing : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string InvoicingType { get; set; } = string.Empty;
		public string ApplicatDepart { get; set; } = string.Empty;
		public string ReasonInvoic { get; set; } = string.Empty;
		public string BillType { get; set; } = string.Empty;
		public string InvoiceAmount { get; set; } = string.Empty;
		public DateTime InvoicDate { get; set; }
		public string InvoicCompany { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

	public class EContractManagement : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string ContractNumber { get; set; } = string.Empty;
		public DateTime SignDate { get; set; }
		public string OurCompany { get; set; } = string.Empty;
		public string OppositeCompany { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string Picture { get; set; } = string.Empty;
		public string ApprovalForm { get; set; } = string.Empty;
		public string ApprovalAnnex { get; set; } = string.Empty;
	}

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

	public class EMeetingRoomApplication : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string AffiliatedCompany { get; set; } = string.Empty;
		public DateTime ApplicationTime { get; set; }
		public DateTime UsageTime { get; set; }
		public string ReasonBorrow { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

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
		public string Picture { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
		public string Remark { get; set; } = string.Empty;
	}

	public class EDeviceManagement : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string BarcodeNumber { get; set; } = string.Empty;
		public string AttributionEngineer { get; set; } = string.Empty;
		public DateTime SampleProcessTime { get; set; }
		public string SampleHandler { get; set; } = string.Empty;
		public string DeviceUsageContent { get; set; } = string.Empty;
		public DateTime DeviceUsageTime { get; set; }
		public string EquipmentUsers { get; set; } = string.Empty;
		public string InternalDuration { get; set; } = string.Empty;
		public string ExternalDuration { get; set; } = string.Empty;
	}

	public class EProjectManagement : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string ItemNumber { get; set; } = string.Empty;
		public string ProjectName { get; set; } = string.Empty;
		public DateTime ProjectStartEndTime { get; set; }
		public string ResearchPersonnel { get; set; } = string.Empty;
		public string ProjectBudget { get; set; } = string.Empty;
		public string ProjectExecution { get; set; } = string.Empty;
		public string InitiationReport { get; set; } = string.Empty;
		public string AnnualReport { get; set; } = string.Empty;
		public string ClosingReport { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

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
		public string AmountOne { get; set; } = string.Empty;
		public string Unit { get; set; } = string.Empty;
		public string AmountTwo { get; set; } = string.Empty;
		public string TotalAmount { get; set; } = string.Empty;
		public string PaymentType { get; set; } = string.Empty;
		public string ProofPayment { get; set; } = string.Empty;
		public string Advances { get; set; } = string.Empty;
		public string ContractOrder { get; set; } = string.Empty;
		public string Remark { get; set; } = string.Empty;
	}

	public class EIssueReceipts : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string InvoiceType { get; set; } = string.Empty;
		public string ApplicatDepart { get; set; } = string.Empty;
		public string ReasonReceipt { get; set; } = string.Empty;
		public string ReceiptAmount { get; set; } = string.Empty;
		public DateTime ReceiptDate { get; set; }
		public string ReceiptCompany { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
	}

	public class EReimbursement : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string TypeExpense { get; set; } = string.Empty;
		public string Amount { get; set; } = string.Empty;
		public string FeeBreakdown { get; set; } = string.Empty;
		public string ExpenseRelated { get; set; } = string.Empty;
		public string Annex { get; set; } = string.Empty;
		public string Remark { get; set; } = string.Empty;
	}

	public class EOrderManagement : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string OrderNumber { get; set; } = string.Empty;
		public string OrderDetails { get; set; } = string.Empty;
		public string SampleNumber { get; set; } = string.Empty;
		public string CustomerInformation { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public string ProjectName { get; set; } = string.Empty;
		public string ProjectLeader { get; set; } = string.Empty;
		public string EquipmentInvolved { get; set; } = string.Empty;
	}

	public class EConsumableManagement : BasicEntity
	{
		public string CorpId { get; set; } = string.Empty;
		public string ConsumableName { get; set; } = string.Empty;
		public string ConsumableLevel { get; set; } = string.Empty;
		public DateTime UsageTime { get; set; }
		public string Purpose { get; set; } = string.Empty;
		public string EquipmentInvolved { get; set; } = string.Empty;
	}
}

