using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal class CostAllocationService(
    IOneCUtService utService,
    IDbContextFactoryProxy dbFactory) : ICostAllocationService
{
    public async Task<CostAllocationDto> GetListAsync(CostAllocationQueryParameters queryParameters, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var items = await dbContext.Set<CostAllocation>()
            .AsNoTracking()
            .HandleCostAllocationQuery(queryParameters)
            .Include(x => x.Author)
            .Include(x => x.Department)
            .Include(x => x.Location)
            .Include(x => x.City)
            .Include(x => x.CostCategory)
                .ThenInclude(c => c!.Manager)
            .Include(x => x.CostItem)
            .ToListAsync(ct);

        var totalItems = await dbContext.Set<CostAllocation>()
            .CountAsync(ct);

        return CostAllocationDto.FromEntities(items, totalItems);
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
