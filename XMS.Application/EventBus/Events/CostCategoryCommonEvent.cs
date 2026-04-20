namespace XMS.Application.EventBus.Events;

public class CostCategoryCommonEvent : IAppEvent
{
    public Guid Ref_Key { get; init; }
    public bool DeletionMark { get; init; }
    public Guid? Parent_Key { get; init; }
    public string? Description { get; init; }
    public string? DataVersion { get; init; }
}
