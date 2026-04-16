using Database.Application.Extensions;

namespace Database.Application.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class OptionalFieldJsonConverter<T> : JsonConverter<OptionalField<T>>
{
    public override OptionalField<T> Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return OptionalField<T>.Some(default);
        }

        var value = JsonSerializer.Deserialize<T>(ref reader, options);
        return OptionalField<T>.Some(value);
    }

    public override void Write(
        Utf8JsonWriter writer,
        OptionalField<T> value,
        JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            writer.WriteNullValue();
            return;
        }

        JsonSerializer.Serialize(writer, value.Value, options);
    }
}