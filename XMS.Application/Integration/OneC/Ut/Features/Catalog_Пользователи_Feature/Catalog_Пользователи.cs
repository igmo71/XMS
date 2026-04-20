namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

public class Catalog_Пользователи : Catalog, ISelectable, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description";
}
