using Microsoft.Extensions.Hosting;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Common;

public class IntegrationHelper
{
    public static string GetUri<T>() where T : ISyncable
    {
        var result = $"{typeof(T).Name}?$format=json&$inlinecount=allpages";

        if (!string.IsNullOrEmpty(T.Select))
            return $"{result}&$select={T.Select}";

        return result;
    }
    public static string GetUriByRefKey<T>(Guid refKey) where T : ISyncable
    {
        var uri = GetUri<T>();
        var result = FilterByRefKey(uri, refKey);
        return result;
    }

    public static string GetUriByDate<T>(DateTime? from = null, DateTime? to = null) where T : ISyncable
    {
        var uri = GetUri<T>();
        var result = FilterUriByDate(uri, from, to);
        return result;
    }

    private static string FilterByRefKey(string uri, Guid refKey) =>
        $"{uri}&$filter=Ref_Key eq guid'{refKey}'";

    private static string FilterUriByDate(string uri, DateTime? from = null, DateTime? to = null) =>
        $"{uri}&$filter=DeletionMark eq false and Posted eq true and Date ge datetime'{from:s}' and Date lt datetime'{to:s}'";

    public static string GetEventName<T>(IntegrationType integrationType, IHostEnvironment hostEnvironment)
    {
        return integrationType switch
        {
            IntegrationType.Notify => hostEnvironment.IsDevelopment() ? $"dev.{typeof(T).Name}.notify" : $"xms.{typeof(T).Name}.notify",
            IntegrationType.Received => hostEnvironment.IsDevelopment() ? $"dev.{typeof(T).Name}.received" : $"xms.{typeof(T).Name}.received",
            IntegrationType.Deleted => hostEnvironment.IsDevelopment() ? $"dev.{typeof(T).Name}.deleted" : $"xms.{typeof(T).Name}.deleted",
            IntegrationType.Outbound => hostEnvironment.IsDevelopment() ? $"dev.{typeof(T).Name}.outbound" : $"xms.{typeof(T).Name}.outbound",
            _ => throw new ArgumentOutOfRangeException(nameof(integrationType), integrationType, null)
        };
    }
}
