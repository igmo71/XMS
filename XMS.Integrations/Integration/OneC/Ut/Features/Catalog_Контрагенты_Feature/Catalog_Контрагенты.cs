using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration.OneC;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public class Catalog_Контрагенты : Catalog, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description,Партнер_Key";

    public Guid? Партнер_Key { get; set; }
}
