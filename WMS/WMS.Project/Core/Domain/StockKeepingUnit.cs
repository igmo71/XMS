using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class StockKeepingUnit : Entity, IHasCode, IHasName
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public Guid BaseUomId { get; private set; }
    public UnitOfMeasure? BaseUom { get; private set; }

    public Dimensions Dimensions { get; set; } = default!;


    private readonly List<Packaging> _packaging = [];
    public IReadOnlyCollection<Packaging> Packaging => _packaging.AsReadOnly();

    public StockKeepingUnit()
    { }

    public StockKeepingUnit(string code, string name, Guid baseUomId, Dimensions dimensions)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? string.Empty;
        BaseUomId = baseUomId;
        Dimensions = dimensions;
    }

    public void AddPackaging(Packaging packaging) => _packaging.Add(packaging);

    public decimal? GetFactorPerBaseUom(Guid uomId) => _packaging.SingleOrDefault(x => x.UomId == uomId)?.FactorPerBaseUom;

    public decimal? ToBaseUomQuantity(Guid uomId, decimal declaredQty) => GetFactorPerBaseUom(uomId) * declaredQty;
}