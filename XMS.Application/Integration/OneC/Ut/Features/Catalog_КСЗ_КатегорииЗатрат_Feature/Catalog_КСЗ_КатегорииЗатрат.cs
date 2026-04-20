using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration.OneC;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

public class Catalog_КСЗ_КатегорииЗатрат : Catalog, IAppEvent
{
    public Guid? Parent_Key { get; set; }
}
