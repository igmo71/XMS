using Microsoft.Extensions.Hosting;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public class Catalog_Контрагенты : ICatalog
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Description { get; set; }
    public Guid? Партнер_Key { get; set; }

    public static string Uri => "Catalog_Контрагенты?$format=json&$inlinecount=allpages" +
        "&$select=Ref_Key,DataVersion,DeletionMark,Parent_Key,IsFolder,Code,Description";

    public static string GetUriByRefKey(Guid refKey) => $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

    public static string GetExchangeName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_Контрагенты)}" : $"{nameof(Catalog_Контрагенты)}";

    public static string GetQueueName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_Контрагенты)}" : $"xms_{nameof(Catalog_Контрагенты)}";
}
