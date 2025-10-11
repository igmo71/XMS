using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Inbound.Domain;

public record InboundOrderStatus(int Id, string Name, string? Description = null) : IState
{
    public IState WithDescription(string description) => this with { Description = description};

    public static readonly InboundOrderStatus Created = new(10, "Created");
    public static readonly InboundOrderStatus Unloading = new(18, "Unloading");
    public static readonly InboundOrderStatus Unloaded = new(20, "Unloaded");
    public static readonly InboundOrderStatus VerificationInProgress = new(28, "VerificationInProgress");
    public static readonly InboundOrderStatus Verified = new(30, "Verified");
    public static readonly InboundOrderStatus PutawayInProgress = new(40, "PutawayInProgress");
    public static readonly InboundOrderStatus PutawayCompleted = new(50, "PutawayCompleted");
    public static readonly InboundOrderStatus Closed = new(60, "Closed");
    public static readonly InboundOrderStatus Rejected = new(70, "Rejected");
}