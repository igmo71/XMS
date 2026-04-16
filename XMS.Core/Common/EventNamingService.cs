using Microsoft.Extensions.Hosting;
using XMS.Core.Abstractions.EventBus;

namespace XMS.Core.Common;

public class EventNamingService(IHostEnvironment hostEnvironment) : IEventNamingService
{
    private readonly string _prefix = hostEnvironment.IsDevelopment() ? "dev" : "xms";

    public string GetEventName(Type eventType) => $"{_prefix}.{eventType.Name}";

    public string DeadLetterName => $"{_prefix}.dead.letter";
}
