using Microsoft.Extensions.Hosting;

namespace XMS.Integration.OneC.Abstractions;

public interface ICatalog
{
    Guid Ref_Key { get; set; }
    string? DataVersion { get; set; }
    bool DeletionMark { get; set; }
    string? Description { get; set; }

    static abstract string Uri { get; }
    static abstract string GetUriByRefKey(Guid refKey);
    static abstract string GetExchangeName(IHostEnvironment hostEnvironment);
    static abstract string GetQueueName(IHostEnvironment hostEnvironment);
}
