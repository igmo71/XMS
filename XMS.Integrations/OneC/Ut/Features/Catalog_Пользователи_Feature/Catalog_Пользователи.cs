using XMS.Application.Abstractions.EventBus;
using XMS.Integrations.OneC;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_Пользователи_Feature;

public class Catalog_Пользователи : Catalog, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description";
}
