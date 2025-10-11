using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain;

public class UnitOfMeasure : Entity, IHasCode, IHasName
{
    public string Code { get; private set; } = default!; // "PCS", "KG", "BOX"
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public UnitOfMeasureType Type { get; private set; } = default!;

    private UnitOfMeasure()
    { }

    public UnitOfMeasure(string code, string name, string? description, UnitOfMeasureType type)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? string.Empty;
        Description = description;
        Type = type;
    }
}
