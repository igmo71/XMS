using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostCategoryIntegrationService
{
    Task PublishAsync(CostCategory item);
}
