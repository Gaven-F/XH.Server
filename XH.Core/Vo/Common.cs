using XH.Core.Database.Entities;
using XH.Core.DataBase.Entities;

namespace XH.Web.Core.Controllers;

public class ProcureApplicationVo : ProcureApplication
{
	new public string Id { get; set; } = string.Empty;
}

public class ProcurementConfirmationVo : ProcurementConfirmation
{
	new public string Id { get; set; } = string.Empty;
}
public class LeaveVo : Leave
{
	new public string Id { get; set; } = string.Empty;
}
public class BusinessTripVo : BusinessTrip
{
	new public string Id { get; set; } = string.Empty;
}
public class MeetingVo : Meeting
{
	new public string Id { get; set; } = string.Empty;
}
public class SealVo : Seal
{
	new public string Id { get; set; } = string.Empty;
}
public class ContractVo : Contract
{
	new public string Id { get; set; } = string.Empty;
}
public class TopicVo : Topic
{
	new public string Id { get; set; } = string.Empty;
}
public class MeetingLogVo : MeetingLog
{
	new public string Id { get; set; } = string.Empty;
}

public class InvoicingVo : Invoicing
{
	new public string Id { get; set; } = string.Empty;
}
public class IssueReceiptsVo : IssueReceipts
{
	new public string Id { get; set; } = string.Empty;
}
public class ChipPaymentVo : ChipPayment
{
	new public string Id { get; set; } = string.Empty;
}
public class ProjectManagementVo : ProjectManagement
{
	new public string Id { get; set; } = string.Empty;
}
public class ReimbursementVo : Reimbursement
{
	new public string Id { get; set; } = string.Empty;
}
public class PaymentVo : Payment
{
	new public string Id { get; set; } = string.Empty;
}
public class OrderManagementVo : OrderManagement
{
	new public string Id { get; set; } = string.Empty;
}

public class ConsumablesManagementVo : ConsumablesManagement
{
	new public string Id { get; set; } = string.Empty;
}
public class EquipmentLogVo : EquipmentLog
{
	new public string Id { get; set; } = string.Empty;
}