using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Inbound.Domain;

public class InboundOrder : Entity, IDocument, IAggregateRoot
{
    public DateTime DateTime { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public string? SourceReference { get; private set; } // ASN id
    public InboundOrderStatus Status { get; private set; } = default!;

    private readonly List<InboundOrderLine> _lines = [];
    public IReadOnlyCollection<InboundOrderLine> Lines => _lines.AsReadOnly();

    private InboundOrder()
    { }

    public InboundOrder(string number, string name, string? sourceReference = null)
    {
        DateTime = DateTime.UtcNow;
        Number = number ?? throw new ArgumentNullException(nameof(number));
        Name = name ?? string.Empty;
        SourceReference = sourceReference;
    }
}
