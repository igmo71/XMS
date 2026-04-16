using XMS.Integration.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

public class Catalog_КСЗ_КатегорииЗатрат_Outbound : IIntegrationEvent
{
    public Guid Ref_Key { get; init; }
    public bool DeletionMark { get; init; }
    public Guid? Parent_Key { get; init; }
    public string? Description { get; init; }
    public string? DataVersion { get; init; }
}
