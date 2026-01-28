using System.ComponentModel.DataAnnotations;

namespace XMS.SharedKernel.Abstractions
{
    public interface IHasId<TId>
    {
        TId Id { get; }
    }

    public interface IHasName
    {
        string Name { get; set; }
    }
}
