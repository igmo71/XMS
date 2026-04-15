using Microsoft.Extensions.Hosting;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal class CostCategoryIntegrationService(IEventPublisher publisher, IHostEnvironment hostEnvironment) : ICostCategoryIntegrationService
{
    public async Task PublishAsync(CostCategory item)
    {
        await publisher.PublishAsync(
            IntegrationHelper.GetEventName<Catalog_КСЗ_КатегорииЗатрат>(IntegrationType.Outbound, hostEnvironment),
            new Catalog_КСЗ_КатегорииЗатрат
            {
                Ref_Key = item.Id,
                DeletionMark = item.IsDeleted,
                Description = item.Name,
                Parent_Key = item.ParentId
            });
    }
}
