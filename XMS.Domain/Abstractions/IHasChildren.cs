using System;
using System.Collections.Generic;
using System.Text;

namespace XMS.Domain.Abstractions
{
    public interface IHasChildren<TDescendant>
    {
        ICollection<TDescendant> Children { get; set; }
    }
}
