using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration.OneC;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

public class Catalog_КонтактныеЛицаПартнеров : Catalog, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Owner_Key,Description";

    public Guid? Owner_Key { get; set; }
}
