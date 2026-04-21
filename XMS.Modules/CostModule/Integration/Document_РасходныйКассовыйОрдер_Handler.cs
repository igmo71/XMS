using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_РасходныйКассовыйОрдер_Handler(
    IDbContextFactoryProxy dbFactory,
    IOneCUtService oneCUtService)
    : IAppEventHandler<Document_РасходныйКассовыйОрдер>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер documentEvent, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        List<Guid> catalog_СтатьяДДС_RefKeys = [];

        if (documentEvent.СтатьяДвиженияДенежныхСредств_Key != null && documentEvent.СтатьяДвиженияДенежныхСредств_Key != Guid.Empty)
            catalog_СтатьяДДС_RefKeys.Add((Guid)documentEvent.СтатьяДвиженияДенежныхСредств_Key);
        else
        {
            catalog_СтатьяДДС_RefKeys = documentEvent.РасшифровкаПлатежа?
                .Where(e => e.СтатьяДвиженияДенежныхСредств_Key != null && e.СтатьяДвиженияДенежныхСредств_Key != Guid.Empty)
                .Select(e => (Guid)e.СтатьяДвиженияДенежныхСредств_Key!)
                .ToList() ?? [];
        }

        foreach (var key in catalog_СтатьяДДС_RefKeys)
        {
            var catalog_СтатьяДДС = await oneCUtService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async(key, ct);
            if (catalog_СтатьяДДС != null)
            {
                var existingCostItem = await dbContext.Set<CostItem>()
                    .FirstOrDefaultAsync(e => e.Id == catalog_СтатьяДДС.Ref_Key, cancellationToken: ct);

                if (existingCostItem == null)
                    await dbContext.Set<CostItem>().AddAsync(new CostItem
                    {
                        Id = catalog_СтатьяДДС.Ref_Key,
                        Name = catalog_СтатьяДДС.Description ?? string.Empty
                    }, ct);
                else
                    existingCostItem.Name = catalog_СтатьяДДС.Description ?? string.Empty;

                if (documentEvent.КСЗ_КатегорияЗатрат_Key != null && documentEvent.КСЗ_КатегорияЗатрат_Key != Guid.Empty)
                {
                    var existingCostCategoryItem = await dbContext.Set<CostCategoryItem>()
                        .FirstOrDefaultAsync(e => e.CategoryId == documentEvent.КСЗ_КатегорияЗатрат_Key && e.ItemId == catalog_СтатьяДДС.Ref_Key, ct);

                    if (existingCostCategoryItem == null)
                        await dbContext.Set<CostCategoryItem>().AddAsync(new CostCategoryItem
                        {
                            CategoryId = (Guid)documentEvent.КСЗ_КатегорияЗатрат_Key,
                            ItemId = catalog_СтатьяДДС.Ref_Key
                        }, ct);

                    var existingCostAllocation = await dbContext.Set<CostAllocation>()
                        .FirstOrDefaultAsync(e => e.PaymentVoucherId == documentEvent.Ref_Key, ct);

                    if (existingCostAllocation is null)
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
                            CostItemId = catalog_СтатьяДДС.Ref_Key,
                            BusinessOperation = documentEvent.ХозяйственнаяОперация,
                            PaymentPurpose = null,
                            AuthorId = documentEvent.Автор_Key,
                            Comment = documentEvent.Комментарий
                        };

                        await dbContext.Set<CostAllocation>().AddAsync(created, ct);
                    }
                    else
                    {
                        existingCostAllocation.IsDeleted = documentEvent.DeletionMark || !documentEvent.Posted;
                        existingCostAllocation.DeletedAt = documentEvent.DeletionMark || !documentEvent.Posted ? DateTime.UtcNow : null;
                        //existingCostAllocation.IsAllocated = false;
                        existingCostAllocation.PaymentVoucherType = PaymentVoucherType.Cash;
                        existingCostAllocation.Number = documentEvent.Number;
                        existingCostAllocation.Date = documentEvent.Date;
                        existingCostAllocation.TotalAmount = documentEvent.СуммаДокумента;
                        existingCostAllocation.CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key;
                        existingCostAllocation.CostItemId = catalog_СтатьяДДС.Ref_Key;
                        existingCostAllocation.BusinessOperation = documentEvent.ХозяйственнаяОперация;
                        existingCostAllocation.PaymentPurpose = null;
                        existingCostAllocation.AuthorId = documentEvent.Автор_Key;
                        existingCostAllocation.Comment = documentEvent.Комментарий;
                    }
                }
            }
        }

        await dbContext.SaveChangesAsync(ct);
    }
}
