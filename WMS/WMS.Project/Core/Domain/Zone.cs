using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class Zone : Entity, IHasCode, IHasName
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public Guid WarefouseId { get; private set; }
    public Warehouse? Warehouse { get; private set; }


    private readonly List<Location> _locations = [];
    public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();


    private Zone()
    { }

    public Zone(string code, string name, Guid warefouseId)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? string.Empty;
        WarefouseId = warefouseId;
    }
}
