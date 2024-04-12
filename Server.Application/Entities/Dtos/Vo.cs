using Server.Domain.ApprocedPolicy;

namespace Server.Application.Entities.Dto;

public class Vo
{
    public class ApproLog : EApprovalLog
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class BussinessTrip : EBussinessTrip
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class ChipPayment : EChipPayment
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class ConsumableManagement : EConsumableManagement
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class ContractManagement : EContractManagement
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class DeviceManagement : EDeviceManagement
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class Invoicing : EInvoicing
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class IssueReceipts : EIssueReceipts
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class Leave : ELeave
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class MeetingMinutes : EMeetingMinutes
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class MeetingRoomApplication : EMeetingRoomApplication
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class Payment : EPayment
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class ProcurementConfirmation : EProcurementConfirmation
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class ProjectManagement : EProjectManagement
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class PurchaseRequest : EPurchaseRequest
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class Reimbursement : EReimbursement
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class UseSeal : EUseSeal
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class ApprovedPolicy : EApprovedPolicy
    {
        new public string Id { get; set; } = string.Empty;
    }

    public class Topic : ETopic
    {
        new public string Id { get; set; } = string.Empty;
    }
}