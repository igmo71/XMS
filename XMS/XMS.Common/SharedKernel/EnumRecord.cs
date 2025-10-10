namespace XMS.Common.SharedKernel;

public abstract record EnumRecord(int Id, string Name, string? Description = null)
{
    public abstract EnumRecord WithDescription(string description);

    public bool Is(string name) => Name == name;
}
