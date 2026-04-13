using System.Text.Json;
using System.Text.Json.Serialization;
using Database.Application.Converters;
using Database.Application.Extensions;

namespace Database.Application.Factories;

public class OptionalFieldJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
               typeToConvert.GetGenericTypeDefinition() == typeof(OptionalField<>);
    }

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
    {
        var innerType = type.GetGenericArguments()[0];

        var converterType = typeof(OptionalFieldJsonConverter<>)
            .MakeGenericType(innerType);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}