using System.Text.Json;
using System.Text.Json.Serialization;

namespace XMS.Core.Common;

public class FlexibleStringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {// Если в JSON пришло число, читаем его как число и конвертируем в строку
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.TryGetDouble(out double doubleValue)
                ? doubleValue.ToString()
                : reader.GetInt64().ToString();
        }

        // Если пришла строка, просто забираем её
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }

        // В остальных случаях (null, bool и т.д.) используем стандартную логику или кидаем ошибку
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            return doc.RootElement.GetRawText();
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}

public class EmptyStringToGuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            return Guid.Empty;
        }
        return Guid.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class StringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (int.TryParse(stringValue, out var result))
            {
                return result;
            }
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32();
        }

        return 0; // Или выбросить исключение, если данные критичны
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
