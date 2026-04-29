using XMS.Application.Abstractions.EventBus;
using XMS.Integrations.OneC;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

public class Catalog_КСЗ_КатегорииЗатрат : Catalog, IAppEvent
{
    public Guid? Parent_Key { get; set; }
}
