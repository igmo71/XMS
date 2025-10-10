using XMS.Common.SharedKernel;

namespace WMS.Project.Core.Domain;

public record ContainerStatus(int Id, string Name, string? Description = null) : EnumRecord(Id, Name, Description)
{
    public override ContainerStatus WithDescription(string description) => this with { Description = description };

    public static readonly ContainerStatus Undefined = new(0, "Undefined");
    public static readonly ContainerStatus Nested = new(1, "Nested");
    public static readonly ContainerStatus Created = new(2, "Created");
    public static readonly ContainerStatus Received = new(3, "Received");
    public static readonly ContainerStatus Stored = new(4, "Stored");
    public static readonly ContainerStatus Picked = new(5, "Picked");
    public static readonly ContainerStatus Shipped = new(6, "Shipped");
    public static readonly ContainerStatus Consumed = new(7, "Consumed ");
    public static readonly ContainerStatus InTransit = new(8, "InTransit");
}