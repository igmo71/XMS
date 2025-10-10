using XMS.Common.SharedKernel;

namespace WMS.Project.Core.Domain;

public record UnitOfMeasureType(int Id, string Name, string? Description = null) : EnumRecord(Id, Name, Description)
{
    public override UnitOfMeasureType WithDescription(string description) => this with { Description = description };

    public static readonly UnitOfMeasureType Base = new(0, "Base");
    public static readonly UnitOfMeasureType Alt = new(1, "Alt");
}