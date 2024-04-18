using System.Text.Json.Serialization;
using Server.Domain.Converters;

namespace Server.Application.Entities;

public class EOrder : BasicEntity
{
    public string? CorpId { get; set; }
    public string? Numbering { get; set; }
    public string? WorkNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? CommissionEngineers { get; set; }
    public string? ContactNumber { get; set; }
    public string? ProductsNumber { get; set; }

    /// <summary>
    /// 样品名称
    /// </summary>
    public string? ProductsName { get; set; }
    public string? ProductsModel { get; set; }
    public string? ProductsLots { get; set; }
    public string? FormFactor { get; set; }
    public string? Scene { get; set; }
    public string? Urgency { get; set; }
    public string? CompletionTime { get; set; }
    public string? Temperature { get; set; }
    public string? Humidity { get; set; }
    public string? Laboratory { get; set; }
    public string? Fabricated { get; set; }
    public string? Review { get; set; }
    [JsonConverter(typeof(JsonLongToStringConverter))]
    public long CompleteOrderId { get; set; }

    /// <summary>
    /// 样品绑定代码
    /// </summary>
    [SugarColumn(IsJson = true)]
    public List<string> Code { get; set; } = [];

    [Navigate(NavigateType.OneToMany, nameof(EOrderItem.OrderId), nameof(Id))]
    public List<EOrderItem>? Items { get; set; }
    public bool StartApprove { get; set; }
    public double TotalTime { get; set; }
}

public class EOrderItem : BasicEntity
{
    [JsonConverter(typeof(JsonLongToStringConverter))]
    public long OrderId { get; set; }
    public string Project { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public string Engineer { get; set; } = string.Empty;
    public string Quantity { get; set; } = string.Empty;
    public string DeviceNumber { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string VirtualStartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string VirtualEndTime { get; set; } = string.Empty;
    public string TrialTime { get; set; } = string.Empty;
    public string VirtualTrialTime { get; set; } = string.Empty;
    public string Sign { get; set; } = string.Empty;
    public string Info { get; set; } = string.Empty;
    public string Operate { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
    public double TotalTime { get; set; }
}
