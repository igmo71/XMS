using System.Text.Json.Serialization;

namespace XMS.Integrations.OneC.Common;

public class RootObject<TValue>
{
    [JsonPropertyName("odata.metadata")] public string? ODataMetadata { get; set; }
    [JsonPropertyName("odata.count")] public string? ODataCount { get; set; }
    [JsonPropertyName("value")] public TValue[]? Value { get; set; }
}
