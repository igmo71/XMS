namespace XMS.Common.SharedKernel.Abstractions;

public interface IState : IHasId<int>, IHasName
{
    public bool Is(string name) => Name == name;

    public bool Is(int id) => Id == id;

    public abstract IState WithDescription(string description);
}

