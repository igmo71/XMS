namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public class Catalog_Контрагенты : Catalog, ISelectable, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description,Партнер_Key";

    public Guid? Партнер_Key { get; set; }
}
