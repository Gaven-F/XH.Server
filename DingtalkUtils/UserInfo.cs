using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Utils.Entity;

public partial class UserInfo
{
    [JsonProperty("corp_id"), System.Text.Json.Serialization.JsonIgnore]
    public string? CorpId { get; set; }

    [JsonProperty("field_data_list")]
    public List<FieldDataList> FieldDataList { get; set; } = [];

    [JsonProperty("userid")]
    public string? UserId { get; set; }
}

public partial class FieldDataList
{
    [JsonProperty("field_code"), System.Text.Json.Serialization.JsonIgnore]
    public string? FieldCode { get; set; }

    [JsonProperty("field_name")]
    public string? FieldName { get; set; }

    [JsonProperty("field_value_list")]
    public List<FieldValueList> FieldValueList { get; set; } = [];

    [JsonProperty("group_id"), System.Text.Json.Serialization.JsonIgnore]
    public GroupId GroupId { get; set; }
}

public partial class FieldValueList
{
    [JsonProperty("item_index"), System.Text.Json.Serialization.JsonIgnore]
    public long ItemIndex { get; set; }

    [
        JsonProperty("label", NullValueHandling = NullValueHandling.Ignore),
        System.Text.Json.Serialization.JsonIgnore
    ]
    public string? Label { get; set; }

    [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
    public string? Value { get; set; }
}

public enum GroupId
{
    Sys,
    Sys00,
    Sys01,
    Sys02,
    Sys05
};

public partial class UserInfo
{
    public static UserInfo FromJson(string json)
    {
        return JsonConvert.DeserializeObject<UserInfo>(json, Converter.Settings)!;
    }
}

public static class Serialize
{
    public static string? ToJson(this UserInfo self)
    {
        return JsonConvert.SerializeObject(self, Converter.Settings);
    }
}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,

        Converters =
        {
            GroupIdConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}

internal class GroupIdConverter : JsonConverter
{

    public override object ReadJson(
        JsonReader reader,
        Type t,
        object? existingValue,
        JsonSerializer serializer
    ) => serializer.Deserialize<string?>(reader) switch
    {
        "sys" => GroupId.Sys,
        "sys00" => GroupId.Sys00,
        "sys01" => GroupId.Sys01,
        "sys02" => GroupId.Sys02,
        "sys05" => (object)GroupId.Sys05,
        _ => throw new Exception("Cannot unmarshal type GroupId"),
    };

    public override void WriteJson(
        JsonWriter writer,
        object? untypedValue,
        JsonSerializer serializer
    )
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (GroupId)untypedValue;
        serializer.Serialize(writer, GroupIdEnumConverter(value));
    }

    public override bool CanConvert(Type t) => t == typeof(GroupId) || t == typeof(GroupId?);
    private static string GroupIdEnumConverter(GroupId value) => value switch
    {
        GroupId.Sys => "sys",
        GroupId.Sys00 => "sys00",
        GroupId.Sys01 => "sys01",
        GroupId.Sys02 => "sys02",
        GroupId.Sys05 => "sys05",
        _ => throw new Exception("Cannot marshal type GroupId"),
    };

    public static readonly GroupIdConverter Singleton = new();
}
