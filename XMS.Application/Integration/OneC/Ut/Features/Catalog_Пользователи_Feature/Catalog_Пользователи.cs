using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration.OneC;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

public class Catalog_Пользователи : Catalog, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description";
}
