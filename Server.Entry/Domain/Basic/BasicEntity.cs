using System.Text.Json.Serialization;
using Server.Domain.Converters;

namespace Server.Domain.Basic;

public class BasicEntity
{
    [SugarColumn(IsPrimaryKey = true)]
    [JsonConverter(typeof(JsonLongToStringConverter))]
    public long Id { get; set; }

    public DateTime CreateTime { get; set; } = DateTime.Now;

    [JsonIgnore]
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
}
