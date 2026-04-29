using XMS.Integrations.OneC;

namespace XMS.Integrations.OneC.Common;

public class IntegrationHelper
{
    public static string GetUri<T>() where T : class, ISelectable
    {
        var result = $"{typeof(T).Name}?$format=json&$inlinecount=allpages";

        if (!string.IsNullOrEmpty(T.Select))
            return $"{result}&$select={T.Select}";

        return result;
    }
    public static string GetUriByRefKey<T>(Guid refKey) where T : class, ISelectable =>
        $"{GetUri<T>()}&$filter=Ref_Key eq guid'{refKey}'";

    public static string GetUriByDate<T>(DateTime? from = null, DateTime? to = null) where T : class, ISelectable =>
        //$"{GetUri<T>()}&$filter=DeletionMark eq false and Posted eq true and Date ge datetime'{from:s}' and Date lt datetime'{to:s}'";
        $"{GetUri<T>()}&$filter=Date ge datetime'{from:s}' and Date lt datetime'{to:s}'";
}
