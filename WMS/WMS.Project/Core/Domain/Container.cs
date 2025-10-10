using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class Container : Entity, IHaveCode// LPM (License Plate Management)
{
    public string Code { get; private set; } = default!; // visible LPN number (e.g., LPN12345)

    public string? SupplierLpnCode { get; private set; }           // если пришёл с завода

    public Guid WarehouseId { get; private set; }
    public Warehouse? Warehouse { get; set; }

    public Guid LocationId { get; private set; }
    public Location? Location { get; set; }

    public ContainerType Type { get; private set; } = default!;
    public ContainerStatus Status { get; private set; } = default!;

    public Guid ParentId { get; private set; }
    public Container? Parent { get; private set; }


    private readonly List<Container> _children = [];
    public IReadOnlyCollection<Container> Children => _children.AsReadOnly();


    private readonly List<ContainerItem> _items = [];
    public IReadOnlyCollection<ContainerItem> Items => _items.AsReadOnly();

    private Container()
    { }

    public Container(string code, Guid warehouseId, Guid location, ContainerType type, string? supplierLpnCode = null)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        WarehouseId = warehouseId;
        LocationId = location;
        Type = type;
        Status = ContainerStatus.Created;
        SupplierLpnCode = supplierLpnCode;
    }

    public void AddItem(ContainerItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (item.ContainerId != Id && item.ContainerId != Guid.Empty)
            throw new InvalidOperationException("Item already assigned to another container");
        // TODO: item.QuantityInBaseUom must be precomputed by IUomConversionService
        _items.Add(item);
    }
}
