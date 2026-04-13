using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

public class Catalog_Пользователи : ICatalog, ISyncable
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Description";

    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Description { get; set; }

}
