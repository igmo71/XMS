using XMS.Common.SharedKernel;
using XMS.Common.SharedKernel.Abstractions;

namespace WMS.Project.Core.Domain
{
    public class ContainerType : Entity, IHaveCode, IHaveName
    {
        public string Code { get; private set; } = default!;
        public string Name { get; private set; } = default!;

        public ContainerType()
        { }

        public ContainerType(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}