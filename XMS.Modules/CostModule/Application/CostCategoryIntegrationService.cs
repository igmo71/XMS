using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal class CostCategoryIntegrationService(IEventPublisher publisher, IEventNamingService eventNaming) : ICostCategoryIntegrationService
{
    public async Task PublishAsync(CostCategory item)
    {
        await publisher.PublishAsync(eventNaming.GetEventName<Catalog_КСЗ_КатегорииЗатрат_Outbound>(),
            new Catalog_КСЗ_КатегорииЗатрат_Outbound
            {
                Ref_Key = item.Id,
                DeletionMark = item.IsDeleted,
                Description = item.Name,
                Parent_Key = item.ParentId
            });
    }
}
