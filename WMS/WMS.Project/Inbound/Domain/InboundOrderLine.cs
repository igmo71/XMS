using WMS.Project.Core.Domain;
using XMS.Common.SharedKernel;

namespace WMS.Project.Inbound.Domain;

public class InboundOrderLine : Entity
{
    public Guid InboundOrderId { get; private set; }
    public InboundOrder? InboundOrder { get; private set; }

    public Guid SkuId { get; private set; }
    public StockKeepingUnit? Sku { get; set; }

    public decimal ExpectedQty { get; private set; }
    public decimal ReceivedQty { get; private set; }

    public Guid UomId { get; private set; } // Unit of Measure
    public UnitOfMeasure? Uom { get; private set; }

    public Guid ContainerId { get; private set; } // License Plate Number
    public Container? Container { get; private set; }
    public InboundOrderLine()
    { }

    public InboundOrderLine(Guid skuId, Guid uomId, decimal expectedQty, Guid containerId)
    {
        SkuId = skuId;
        UomId = uomId;
        ExpectedQty = expectedQty;
        ReceivedQty = 0;
        ContainerId = containerId;
    }
}