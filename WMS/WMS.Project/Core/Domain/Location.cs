using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class Location : Entity, IHasCode
{
    public string Code { get; private set; } = default!;

    public Guid ZoneId { get; private set; }
    public Zone? Zone { get; set; }

    public Dimensions Capacity { get; private set; } = default!;
    public Coordinates Coordinates { get; private set; } = default!;

    private Location()
    { }

    public Location(string code, Guid zoneId, Dimensions capacity, Coordinates coordinates)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        ZoneId = zoneId;
        Capacity = capacity;
        Coordinates = coordinates;
    }
}
