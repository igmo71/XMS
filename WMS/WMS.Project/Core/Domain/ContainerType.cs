using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain
{
    public class ContainerType : Entity, IHasCode, IHasName
    {
        public string Code { get; private set; } = default!;
        public string Name { get; private set; } = default!;

        public ContainerType()
        { }

        public ContainerType(string code, string name)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? string.Empty;
        }
    }
}