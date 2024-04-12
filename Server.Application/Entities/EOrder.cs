using Server.Domain.Basic;
using Server.Domain.Converters;
using SqlSugar;
using System.Text.Json.Serialization;

namespace Server.Application.Entities;

public class EOrder : BasicEntity
{
    public string? CorpId { get; set; }
    public string? Numbering { get; set; }
    public string? WorkNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? CommissioneEngineers { get; set; }
    public string? ContactNumber { get; set; }
    public string? ProductsNumber { get; set; }
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

    [Navigate(NavigateType.OneToMany, nameof(EOrderItem.OrderId), nameof(Id))]
    public List<EOrderItem>? Items { get; set; }
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
}