using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииСтатейЗатрат_Feature;

public class Catalog_КСЗ_КатегорииСтатейЗатрат : ICatalog, IOneCEvent
{
    public Guid Ref_Key { get; set; }
    public bool DeletionMark { get; set; }
    public Guid? Parent_Key { get; set; }
    public string? Description { get; set; }
    public string? DataVersion { get; set; }
}
