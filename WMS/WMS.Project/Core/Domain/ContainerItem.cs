using XMS.Common.SharedKernel;

namespace WMS.Project.Core.Domain;

public class ContainerItem : Entity
{
    public Guid ContainerId { get; set; }
    public Container? Container { get; set; }

    public Guid SkuId { get; private set; }
    public StockKeepingUnit? Sku { get; private set; }

    public Guid DeclaredUomId { get; private set; }
    public UnitOfMeasure? DeclaredUom { get; private set; }

    public decimal DeclaredQuantity { get; private set; }

    public decimal QuantityInBaseUom { get; private set; }

    public ContainerItem()
    { }

    public ContainerItem(Guid containerId, Guid skuId, Guid declaredUomId, decimal declaredQuantity, decimal quantityInBaseUom)
    {
        ContainerId = containerId;
        SkuId = skuId;
        DeclaredUomId = declaredUomId;
        DeclaredQuantity = declaredQuantity;
        QuantityInBaseUom = quantityInBaseUom;
    }
}