namespace Server.Entry.Utils;

public class CopyMsgFormat
{
    public static readonly Func<object, string> LFormate = (object data) =>
    {
        if (data is ELeave entity)
        {
            return string.Format(
                $$""""
                {{"请假类型：",-6}}{0,10}
                {{"剩余天数：",-6}}{1,10} 
                {{"开始时间：",-6}}{2,10}（{3,10}）
                {{"结束时间：",-6}}{4,10}（{5,10}）
                {{"时长：",-4}}{6,10}
                {{"事由：",-4}}{7,10}
                """",
                entity.LeaveType,
                entity.AnnualLeave,
                entity.StartDate,
                entity.StartDateSuffix,
                entity.EndDate,
                entity.EndDateSuffix,
                entity.SumTime,
                entity.ReasonLeave
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> BTFormate = (object data) =>
    {
        if (data is EBusinessTrip entity)
        {
            return string.Format(
                $$""""
                {{"出差类别：",-6}}{0,10}
                {{"出发地：",-6}}{1,10} 
                {{"目的地：",-6}}{2,10} 
                {{"交通工具：",-6}}{3,10}
                {{"开始时间：",-6}}{4,10}（{5,10}）
                {{"结束时间：",-6}}{6,10}（{7,10}） 
                {{"时长：",-4}}{8,10}
                {{"同行人：",-4}}{9,10}
                {{"事由：",-4}}{10,10}
                """",
                entity.TravelType,
                entity.PlaceDeparture,
                entity.Destination,
                entity.Vehicle,
                entity.StartDate,
                entity.StartDateSuffix,
                entity.EndDate,
                entity.EndDateSuffix,
                entity.SumTime,
                entity.Colleague,
                entity.BusinessTrip
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> PRFormate = (object data) =>
    {
        if (data is EPurchaseRequest entity)
        {
            return string.Format(
                $$""""
                {{"采购项目：",-6}}{0,10} 
                {{"采购类型：",-6}}{1,10} 
                {{"经费支出说明：",-6}}{2,10} 
                {{"预计采购日期：",-6}}{3,10} 
                {{"总金额：",-4}}{4,10}
                {{"备注：",-4}}{5,10}
                {{"采购方式：",-4}}{6,10}
                """",
                entity.ProcureProject,
                entity.ProcurType,
                entity.ExplanatExpenditure,
                entity.PurchaseDate,
                entity.TotalAmount,
                entity.Notes,
                entity.ProcureMethod
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> PCFormate = (object data) =>
    {
        if (data is EProcurementConfirmation entity)
        {
            return string.Format(
                $$""""
                {{"采购项目说明：",-6}}{0,10} 
                {{"采购类型：",-6}}{1,10} 
                {{"交付时间：",-6}}{2,10} 
                {{"采购明细：",-6}}{3,10} 
                {{"名称：",-4}}{4,10}
                {{"数量：",-4}}{5,10}
                {{"单位：",-4}}{6,10}
                {{"单价：",-4}}{7,10}
                {{"支付方式：",-4}}{8,10}
                """",
                entity.Description,
                entity.ProcurType,
                entity.DeliveryTime,
                entity.ProcurementDetails,
                entity.ProcurName,
                entity.PurchaseQuantity,
                entity.Unit,
                entity.UnitPrice,
                entity.PaymentMethod
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> USFormate = (object data) =>
    {
        if (data is EUseSeal entity)
        {
            return string.Format(
                $$""""
                {{"用印类型：",-6}}{0,10} 
                {{"申请部门：",-6}}{1,10} 
                {{"经办人：",-6}}{2,10} 
                {{"使用日期：",-6}}{3,10} 
                {{"事由：",-4}}{4,10}
                {{"印章类型：",-4}}{5,10}
                """",
                entity.StampType,
                entity.ApplicatDepart,
                entity.HandledBy,
                entity.Date,
                entity.ReasonBorrow,
                entity.SealType
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> EFormate = (object data) =>
    {
        if (data is ETopic entity)
        {
            return string.Format(
                $$""""
                {{"申请部门：",-6}}{0,10} 
                {{"议题标题：",-6}}{1,10} 
                {{"议题内容：",-6}}{2,10} 
                {{"审议会议：",-6}}{3,10} 
                {{"会议时间：",-4}}{4,10}
                {{"参会人员：",-4}}{5,10}
                {{"备注：",-4}}{6,10}
                """",
                entity.ApplicatDepart,
                entity.TopicTitle,
                entity.TopicContent,
                entity.ReviewConference,
                entity.MeetingTime,
                entity.Participants,
                entity.Notes
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> MMFormate = (object data) =>
    {
        if (data is EMeetingMinutes entity)
        {
            return string.Format(
                $$""""
                {{"会议编号：",-6}}{0,10} 
                {{"会议纪要名称：",-6}}{1,10} 
                {{"开会日期：",-6}}{2,10} 
                {{"备注：",-6}}{3,10} 
                """",
                entity.MeetingNumber,
                entity.SummaryName,
                entity.MeetingDate,
                entity.Notes
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> CMFormate = (object data) =>
    {
        if (data is EContractManagement entity)
        {
            return string.Format(
                $$""""
                {{"合同编号：",-6}}{0,10} 
                {{"签约日期：",-6}}{1,10} 
                {{"我方单位名称：",-6}}{2,10} 
                {{"对方单位名称：",-6}}{3,10} 
                {{"内容：",-4}}{4,10}
                """",
                entity.ContractNumber,
                entity.SignDate,
                entity.OurCompany,
                entity.OppositeCompany,
                entity.Content
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> SIFormate = (object data) =>
    {
        if (data is ESalaryInfo entity)
        {
            return string.Format(
                $$""""
                {{"金额：",-6}}{0,10} 
                {{"理由：",-6}}{1,10}
                {{"时间：",-6}}{2,10} 
                """",
                entity.Amount,
                entity.MainContent,
                entity.StartDate
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> IFormate = (object data) =>
    {
        if (data is EInvoicing entity)
        {
            return string.Format(
                $$""""
                {{"合同编号：",-6}}{0,10} 
                {{"开票类型：",-6}}{1,10}
                {{"申请部门：",-6}}{3,10} 
                {{"开票事由：",-6}}{4,10} 
                {{"开票金额：",-6}}{5,10}
                {{"开票日期：",-6}}{6,10} 
                {{"开票公司名称：",-6}}{7,10} 
                """",
                entity.ContractNumber,
                entity.InvoicingType,
                entity.ApplicatDepart,
                entity.ReasonInvoic,
                entity.InvoiceAmount,
                entity.InvoicDate,
                entity.InvoicCompany
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> IRFormate = (object data) =>
    {
        if (data is EIssueReceipts entity)
        {
            return string.Format(
                $$""""
                {{"开收据类型：",-6}}{0,10} 
                {{"申请部门：",-6}}{1,10}
                {{"开收据事由：",-6}}{3,10} 
                {{"开收据金额：",-6}}{4,10} 
                {{"开收据日期：",-6}}{5,10}
                {{"开收据公司名称：",-6}}{6,10} 
                """",
                entity.InvoiceType,
                entity.ApplicantDepart,
                entity.ReasonReceipt,
                entity.ReceiptAmount,
                entity.ReceiptDate,
                entity.ReceiptCompany
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> RFormate = (object data) =>
    {
        if (data is EReimbursement entity)
        {
            return string.Format(
                $$""""
                {{"费用类型：",-6}}{0,10} 
                {{"金额：",-6}}{1,10}
                {{"费用明细：",-6}}{3,10} 
                {{"备注：",-6}}{4,10} 
                """",
                entity.TypeExpense,
                entity.Amount,
                entity.FeeBreakdown,
                entity.Remark
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> PFormate = (object data) =>
    {
        if (data is EPayment entity)
        {
            return string.Format(
                $$""""
                {{"合同编号：",-6}}{0,10} 
                {{"付款事由：",-6}}{1,10} 
                {{"付款类型：",-6}}{2,10} 
                {{"金额：",-6}}{3,10} 
                {{"付款方式：",-4}}{4,10}
                {{"支付日期：",-4}}{5,10}
                {{"采购明细：",-4}}{6,10}
                {{"收款单位名称：",-6}}{7,10} 
                {{"开户行：",-4}}{8,10}
                {{"银行账号：",-4}}{9,10}
                {{"付款说明：",-4}}{10,10}
                {{"供应商开票情况：",-4}}{11,10}
                {{"备注：",-4}}{12,10}
                """",
                entity.ContractNumber,
                entity.ReasonPayment,
                entity.PaymentType,
                entity.Amount,
                entity.PaymentMethods,
                entity.DatePayment,
                entity.PurchaseDetails,
                entity.ReceivingUnit,
                entity.Bank,
                entity.Account,
                entity.PaymentInstructions,
                entity.Invoicing,
                entity.Remark
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> PMFormate = (object data) =>
    {
        if (data is EProjectManagement entity)
        {
            return string.Format(
                $$""""
                {{"项目编号：",-6}}{0,10} 
                {{"项目名称：",-6}}{1,10} 
                {{"项目开始时间：",-6}}{2,10} 
                {{"项目结束时间：",-6}}{3,10} 
                {{"研发人员：",-4}}{4,10}
                {{"项目预算：",-4}}{5,10}
                {{"项目实际执行数：",-4}}{6,10}
                """",
                entity.ItemNumber,
                entity.ProjectName,
                entity.ProjectStartTime,
                entity.ProjectEndTime,
                entity.ResearchPersonnel,
                entity.ProjectBudget,
                entity.ProjectExecution
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> TCFormate = (object data) =>
    {
        if (data is ET_C entity)
        {
            return string.Format(
                $$""""
                {{"A-合同编号：",-6}}{0,10} 
                {{"签约日期：",-6}}{1,10} 
                {{"我方单位名称：",-6}}{2,10} 
                {{"对方单位名称：",-6}}{3,10} 
                {{"内容：",-4}}{4,10}
                {{"B-合同编号：",-6}}{5,10} 
                {{"签约日期：",-6}}{6,10} 
                {{"我方单位名称：",-6}}{7,10} 
                {{"对方单位名称：",-6}}{8,10} 
                {{"内容：",-4}}{9,10}
                {{"合同差价：",-4}}{10,10}
                {{"申请部门：",-6}}{11,10} 
                {{"议题标题：",-6}}{12,10} 
                {{"议题内容：",-6}}{13,10} 
                {{"审议会议：",-6}}{14,10} 
                {{"会议时间：",-4}}{15,10}
                {{"参会人员：",-6}}{16,10} 
                {{"备注：",-6}}{17,10} 
                """",
                entity.C_ContractNumber_A,
                entity.C_SignDate_A,
                entity.C_OurCompany_A,
                entity.C_OppositeCompany_A,
                entity.C_Content_A,
                entity.C_ContractNumber_B,
                entity.C_SignDate_B,
                entity.C_OurCompany_B,
                entity.C_OppositeCompany_B,
                entity.C_Content_B,
                entity.T_ApplicantDepart,
                entity.T_TopicTitle,
                entity.T_TopicContent,
                entity.T_ReviewConference,
                entity.T_MeetingTime,
                entity.T_Participants,
                entity.T_Notes
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> CPFormate = (object data) =>
    {
        if (data is EChipPayment entity)
        {
            return string.Format(
                $$""""
                {{"合同编号：",-6}}{0,10} 
                {{"付款事由：",-6}}{1,10} 
                {{"支付日期：",-6}}{2,10} 
                {{"支付方式：",-6}}{3,10} 
                {{"收款单位名称：",-4}}{4,10}
                {{"开户行：",-6}}{5,10} 
                {{"银行账号：",-6}}{6,10} 
                {{"汇率选择：",-6}}{7,10} 
                {{"金额：",-6}}{8,10} 
                {{"付款类型：",-4}}{9,10}
                {{"垫资情况说明：",-6}}{10,10} 
                {{"备注",-6}}{11,10} 
                """",
                entity.ContractNumber,
                entity.ReasonPayment,
                entity.DatePayment,
                entity.PaymentMethods,
                entity.ReceivingUnit,
                entity.Bank,
                entity.Account,
                entity.RateSelection,
                entity.Unit,
                entity.Amount,
                entity.PaymentType,
                entity.Advances,
                entity.Remark
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };
    public static readonly Func<object, string> DMFormate = (object data) =>
    {
        if (data is EDeviceManagement entity)
        {
            return string.Format(
                $$""""
                {{"条码编号：",-6}}{0,10} 
                {{"工程师：",-6}}{1,10}
                {{"样品处理：",-6}}{2,10} 
                """",
                entity.BarcodeNumber,
                entity.AttributionEngineer,
                entity.SampleHandler
            );
        }
        else
        {
            throw new ArgumentException();
        }
    };

    public static readonly Dictionary<string, Func<object, string>> FormatMapper =
        new()
        {
            { nameof(ELeave), LFormate },
            { nameof(EBusinessTrip), BTFormate },
            { nameof(EPurchaseRequest), PRFormate },
            { nameof(EProcurementConfirmation), PCFormate },
            { nameof(EUseSeal), USFormate },
            { nameof(ETopic), EFormate },
            { nameof(EMeetingMinutes), MMFormate },
            { nameof(EContractManagement), CMFormate },
            { nameof(ESalaryInfo), SIFormate },
            { nameof(EDeviceManagement), DMFormate },
            { nameof(EInvoicing), IFormate },
            { nameof(EIssueReceipts), IRFormate },
            { nameof(EReimbursement), RFormate },
            { nameof(EPayment), PFormate },
            { nameof(EProjectManagement), PMFormate },
            { nameof(ET_C), TCFormate },
            { nameof(EChipPayment), CPFormate },
        };
}
