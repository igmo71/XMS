using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class Zone : Entity, IHaveCode
{
    public string Code { get; private set; } = default!;

    public Guid WarefouseId { get; private set; }
    public Warehouse? Warehouse { get; private set; }


    private readonly List<Location> _locations = [];
    public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();

    private Zone()
    { }

    public Zone(string code, Guid warefouseId)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        WarefouseId = warefouseId;
    }
}
