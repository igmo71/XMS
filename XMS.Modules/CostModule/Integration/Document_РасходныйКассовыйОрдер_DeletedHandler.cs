using Microsoft.EntityFrameworkCore;
using XMS.Application.EventBus.Events;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_РасходныйКассовыйОрдер_DeletedHandler(IDbContextFactoryProxy dbFactory)
    : IAppEventHandler<Document_РасходныйКассовыйОрдер_Deleted>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер_Deleted deleted, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>().FirstOrDefaultAsync(e => e.PaymentVoucherId == deleted.Ref_Key, ct);

        if (existing is not null)
        {
            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
