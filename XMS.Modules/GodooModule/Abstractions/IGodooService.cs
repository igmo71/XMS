namespace XMS.Modules.GodooModule.Abstractions;

public interface IGodooService
{
    Task Reload(string apiKeyName, CancellationToken ct);
}
