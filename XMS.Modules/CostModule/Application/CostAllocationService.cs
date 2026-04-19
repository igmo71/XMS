using Microsoft.EntityFrameworkCore;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal class CostAllocationService(
    IOneCUtService utService,
    IDbContextFactoryProxy dbFactory) : ICostAllocationService
{
    public async Task<IReadOnlyList<CostAllocation>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var query = dbContext.Set<CostAllocation>()
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Manager)
            .Include(x => x.Department)
            .Include(x => x.Location)
            .Include(x => x.City)
            .Include(x => x.CostCategory)
            .Include(x => x.CostItem)
            .AsQueryable();

        if (!includeDeleted)
            query = query.Where(x => !x.IsDeleted);

        return await query
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Number)
            .ToListAsync(ct);
    }

    public async Task UpdateAsync(CostAllocation item, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>()
            .FirstOrDefaultAsync(x => x.Id == item.Id, ct)
            ?? throw new KeyNotFoundException($"CostAllocation with ID {item.Id} not found");

        dbContext.UpdateValues(existing, item);

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Document_РасходныйКассовыйОрдер>> GetDocumentРасходныйКассовыйОрдерAsync(
        DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        return await utService.GetDocument_РасходныйКассовыйОрдер_Async(parameters, ct);
    }

    public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(
        DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        return await utService.GetDocument_СписаниеБезналичныхДенежныхСредств_Async(parameters, ct);
    }
}
