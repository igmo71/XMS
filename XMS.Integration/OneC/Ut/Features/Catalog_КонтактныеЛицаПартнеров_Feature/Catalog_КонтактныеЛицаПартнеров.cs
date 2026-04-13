using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

public class Catalog_КонтактныеЛицаПартнеров : ICatalog, ISyncable
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Owner_Key,Description";

    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public Guid? Owner_Key { get; set; }
    public string? Description { get; set; }
}
