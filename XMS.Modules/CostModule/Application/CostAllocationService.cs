using Microsoft.EntityFrameworkCore;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Api;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal class CostAllocationService(IOneCUtService utService, IDbContextFactoryProxy dbFactory) : ICostAllocationService
{
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

    public async Task HandleDocument_РасходныйКассовыйОрдер_ReceivedAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>().FirstOrDefaultAsync(e => e.PaymentVoucherId == dto.Ref_Key, ct);

        if (existing is null)
        {
            var created = new CostAllocation
            {
                PaymentVoucherId = dto.Ref_Key,
                PaymentVoucherType = PaymentVoucherType.Bank,
                IsAllocated = false,
                Number = dto.Number,
                Date = dto.Date,
                TotalAmount = dto.СуммаДокумента,
                PaymentPurpose = dto.ХозяйственнаяОперация,
                CostCategoryId = dto.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : dto.КСЗ_КатегорияЗатрат_Key,
                Catalog_СтатьяДДС_Key = dto.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : dto.СтатьяДвиженияДенежныхСредств_Key,
                BusinessOperation = dto.ХозяйственнаяОперация,
                AuthorId = dto.Автор_Key,
                Comment = dto.Комментарий
            };

            await dbContext.Set<CostAllocation>().AddAsync(created, ct);
        }
        else
        {
            existing.IsAllocated = false;
            existing.Number = dto.Number;
            existing.Date = dto.Date;
            existing.TotalAmount = dto.СуммаДокумента;
            existing.PaymentPurpose = dto.ХозяйственнаяОперация;
            existing.CostCategoryId = dto.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : dto.КСЗ_КатегорияЗатрат_Key;
            existing.Catalog_СтатьяДДС_Key = dto.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : dto.СтатьяДвиженияДенежныхСредств_Key;
            existing.BusinessOperation = dto.ХозяйственнаяОперация;
            existing.AuthorId = dto.Автор_Key;
            existing.Comment = dto.Комментарий;
        }

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task HandleDocument_РасходныйКассовыйОрдер_DeletedAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default)
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
