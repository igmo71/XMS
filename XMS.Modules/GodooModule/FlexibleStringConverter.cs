using System.Text.Json;
using System.Text.Json.Serialization;

namespace XMS.Modules.GodooModule
{
    internal class FlexibleStringConverter : JsonConverter<string>
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
}
