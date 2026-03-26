using Microsoft.Extensions.Hosting;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

public class Catalog_КонтактныеЛицаПартнеров : ICatalog
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public Guid? Owner_Key { get; set; }
    public string? Description { get; set; }

    public static string Uri =>
        "Catalog_КонтактныеЛицаПартнеров?$format=json&$inlinecount=allpages&$select=Ref_Key,DataVersion,DeletionMark,Owner_Key,Description";

    public static string GetUriByRefKey(Guid refKey) => $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

    public static string GetExchangeName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_КонтактныеЛицаПартнеров)}" : $"{nameof(Catalog_КонтактныеЛицаПартнеров)}";

    public static string GetQueueName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_КонтактныеЛицаПартнеров)}" : $"xms_{nameof(Catalog_КонтактныеЛицаПартнеров)}";
}
