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
            return new OptionalField<T>(default);
        }

        var value = JsonSerializer.Deserialize<T>(ref reader, options);
        return new OptionalField<T>(value);
    }

    public override void Write(
        Utf8JsonWriter writer,
        OptionalField<T> value,
        JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            return;
        }

        JsonSerializer.Serialize(writer, value.Value, options);
    }
}