﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Domain.Converters;

public class JsonLongToStringConverter : JsonConverter<long>
{
    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (
            reader.TokenType == JsonTokenType.String
            && long.TryParse(reader.GetString(), out long result)
        )
        {
            return result;
        }

        return reader.GetInt64();
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
