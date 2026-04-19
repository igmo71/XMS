using Microsoft.Extensions.Hosting;

namespace XMS.Application.Core.Common;

public class EventNamingService(IHostEnvironment hostEnvironment) : IEventNamingService
{
    private readonly string _prefix = hostEnvironment.IsDevelopment() ? "dev" : "xms";

    public string GetEventName(Type eventType) => $"{_prefix}.{eventType.Name}";

    public string DeadLetterName => $"{_prefix}.dead.letter";
}
