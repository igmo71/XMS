using Microsoft.Extensions.Hosting;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

public class Catalog_Пользователи : ICatalog
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Description { get; set; }

    public static string Uri => "Catalog_Пользователи?$format=json&$inlinecount=allpages&$select=Ref_Key,DataVersion,DeletionMark,Description";

    public static string GetUriByRefKey(Guid refKey) => $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

    public static string GetExchangeName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_Пользователи)}" : $"{nameof(Catalog_Пользователи)}";

    public static string GetQueueName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_Пользователи)}" : $"xms_{nameof(Catalog_Пользователи)}";
}
