using Server.Domain.ApprocedPolicy;

namespace Server.Application.Entities.Dto;

public class Vo
{
    public class ApproLog : EApprovalLog
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class BussinessTrip : EBussinessTrip
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class ChipPayment : EChipPayment
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class ConsumableManagement : EConsumableManagement
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class ContractManagement : EContractManagement
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class DeviceManagement : EDeviceManagement
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class Invoicing : EInvoicing
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class IssueReceipts : EIssueReceipts
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class Leave : ELeave
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class MeetingMinutes : EMeetingMinutes
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class MeetingRoomApplication : EMeetingRoomApplication
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class Payment : EPayment
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class ProcurementConfirmation : EProcurementConfirmation
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class ProjectManagement : EProjectManagement
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class PurchaseRequest : EPurchaseRequest
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class Reimbursement : EReimbursement
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class UseSeal : EUseSeal
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class ApprovedPolicy : EApprovedPolicy
    {
        public new string Id { get; set; } = string.Empty;
    }

    public class Topic : ETopic
    {
        public new string Id { get; set; } = string.Empty;
    }
}
