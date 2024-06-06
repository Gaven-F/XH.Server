namespace Server.Entry.Utils;

public class CopyMsgFormat
{
    public static readonly Func<object, string> BTFormate = (object data) =>
    {
        
        if (data is EBusinessTrip entity)
        {
            return string.Format(
                $$""""
                {{"出差类别：",-6}}{0,10} {{"出发地：",-6}}{1,10} 
                {{"交通工具：",-6}}{2,10} {{"目的地：",-6}}{3,10} 
                {{"同僚：",-4}}{4,10}
                {{"事由：",-4}}{5,10}
                """",
                entity.TravelType,
                entity.PlaceDeparture,
                entity.Vehicle,
                entity.Destination,
                entity.Colleague,
                entity.BusinessTrip
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
                {{"合同编号：",-6}}{0,10} {{"付款原因：",-6}}{1,10} 
                {{"付款方式：",-6}}{2,10} {{"接受单位：",-6}}{3,10} 
                {{"金额：",-4}}{4,10}
                {{"备注：",-4}}{5,10}
                """",
                entity.ContractNumber,
                entity.ReasonPayment,
                entity.PaymentMethods,
                entity.Unit,
                entity.Amount,
                entity.Remark
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
                {{"合同名称：",-6}}{0,10} 
                {{"本公司：",-6}}{1,10}
                {{"对方公司：",-6}}{2,10} 
                {{"内容：",-4}}{3,10}
                """",
                entity.ContractNumber,
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
            { nameof(EBusinessTrip), BTFormate },
            { nameof(EChipPayment), CPFormate },
            { nameof(EContractManagement), CMFormate },
            { nameof(EDeviceManagement), DMFormate },
        };
}
