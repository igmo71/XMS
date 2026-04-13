using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public class Catalog_Контрагенты : ICatalog, ISyncable
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description,Партнер_Key";

    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Description { get; set; }
    public Guid? Партнер_Key { get; set; }
}
