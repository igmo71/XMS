namespace XMS.Application.Integration.OneC.Common;

public record CatalogNotification : IIntegrationEvent
{
    public Guid Ref_Key { get; init; }
    public string? DataVersion { get; init; }
    public bool DeletionMark { get; init; }

    public string? Description { get; init; }
}
