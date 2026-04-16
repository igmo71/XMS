using Microsoft.EntityFrameworkCore;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_РасходныйКассовыйОрдер_DeletedHandler(IDbContextFactoryProxy dbFactory)
    : IIntegrationEventHandler<Document_РасходныйКассовыйОрдер_Dto>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>().FirstOrDefaultAsync(e => e.PaymentVoucherId == dto.Ref_Key, ct);

        if (existing is not null)
        {
            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
