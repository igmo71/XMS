using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class Packaging : Entity, IHaveCode, IHaveName
{
    public string Code { get; private set; } = default!; // "BOX100" or "CASE10KG" (catalog code)
    public string Name { get; private set; } = default!;

    public Guid SkuId { get; private set; }
    public StockKeepingUnit? Sku { get; private set; }

    public Guid UomId { get; private set; }   // e.g. BOX
    public UnitOfMeasure? Uom { get; private set; }

    public decimal FactorPerBaseUom { get; private set; } // 1 BOX = 100 PCS => FactorPerBaseUom = 100

    public int Level { get; private set; } // 0 = base, 1 = box, 2 = case, etc.

    public Dimensions Dimensions { get; private set; } = default!;

    public Guid? ParentId { get; private set; }
    public Packaging? Parent { get; private set; }


    private readonly List<Packaging> _children = [];
    public IReadOnlyCollection<Packaging> Children => _children.AsReadOnly();

    private Packaging() { }

    public Packaging(string code, string name, Guid skuId, Guid uomId, decimal unitsPerBaseUom, int level, Dimensions dimensions, Guid? parentId = null)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name;
        SkuId = skuId;
        UomId = uomId;
        FactorPerBaseUom = unitsPerBaseUom;
        Level = level;
        Dimensions = dimensions;
        ParentId = parentId;
    }

    public void AddChild(Packaging child)
    {
        child.ParentId = ParentId;

        _children.Add(child);
    }
}
