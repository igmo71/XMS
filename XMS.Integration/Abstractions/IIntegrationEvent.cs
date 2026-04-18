namespace XMS.Integration.Abstractions;

public interface IIntegrationEvent //: IAppEvent
{
    Guid Ref_Key { get; init; }
    string? DataVersion { get; init; }
    public bool DeletionMark { get; init; }
}
