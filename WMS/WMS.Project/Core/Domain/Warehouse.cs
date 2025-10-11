using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class Warehouse : Entity, IHasCode, IHasName
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private readonly List<Zone> _zones = [];
    public IReadOnlyCollection<Zone> Zones => _zones.AsReadOnly();

    private Warehouse()
    { }

    public Warehouse(string code, string name)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? string.Empty;
    }
}
