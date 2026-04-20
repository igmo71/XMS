using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_РасходныйКассовыйОрдер_Handler(
    IDbContextFactoryProxy dbFactory,
    ICostItemService costItemService,
    ICostCategoryItemService costCategoryItemService,
    IOneCUtService oneCUtService)
    : IAppEventHandler<Document_РасходныйКассовыйОрдер>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер documentEvent, CancellationToken ct = default)
    {
        if (documentEvent.СтатьяДвиженияДенежныхСредств_Key != null)
        {
            var catalog_СтатьяДДС = await oneCUtService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async((Guid)documentEvent.СтатьяДвиженияДенежныхСредств_Key, ct);

            if (catalog_СтатьяДДС?.Description != null)
            {
                await costItemService.CreateAsync(new CostItem
                {
                    Id = (Guid)documentEvent.СтатьяДвиженияДенежныхСредств_Key,
                    Name = catalog_СтатьяДДС.Description
                }, ct);

                if (documentEvent.КСЗ_КатегорияЗатрат_Key != null)
                    await costCategoryItemService.CreateAsync((Guid)documentEvent.КСЗ_КатегорияЗатрат_Key, (Guid)documentEvent.СтатьяДвиженияДенежныхСредств_Key, ct);
            }
        }

        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>().FirstOrDefaultAsync(e => e.PaymentVoucherId == documentEvent.Ref_Key, ct);

        if (existing is null)
        {
            var created = new CostAllocation
            {
                IsAllocated = false,
                PaymentVoucherId = documentEvent.Ref_Key,
                PaymentVoucherType = PaymentVoucherType.Cash,
                Number = documentEvent.Number,
                Date = documentEvent.Date,
                TotalAmount = documentEvent.СуммаДокумента,
                CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key,
                CostItemId = documentEvent.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : documentEvent.СтатьяДвиженияДенежныхСредств_Key,
                BusinessOperation = documentEvent.ХозяйственнаяОперация,
                AuthorId = documentEvent.Автор_Key,
                Comment = documentEvent.Комментарий
            };

            await dbContext.Set<CostAllocation>().AddAsync(created, ct);
        }
        else
        {
            existing.IsDeleted = documentEvent.DeletionMark;
            existing.DeletedAt = DateTime.UtcNow; ;
            existing.IsAllocated = false;
            existing.PaymentVoucherType = PaymentVoucherType.Cash;
            existing.Number = documentEvent.Number;
            existing.Date = documentEvent.Date;
            existing.TotalAmount = documentEvent.СуммаДокумента;
            existing.PaymentPurpose = documentEvent.ХозяйственнаяОперация;
            existing.CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key;
            existing.CostItemId = documentEvent.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : documentEvent.СтатьяДвиженияДенежныхСредств_Key;
            existing.BusinessOperation = documentEvent.ХозяйственнаяОперация;
            existing.AuthorId = documentEvent.Автор_Key;
            existing.Comment = documentEvent.Комментарий;
        }

        await dbContext.SaveChangesAsync(ct);
    }
}
